-- Dimension tables:

--====================================================================================================================--
-- Prerequisites:

-- Import SQL source data (pa220-cars.sql and pa220-data.sql (this file in an UNLOGGED table mode!))

-- E.g. local command can be in cmd.exe: 
-- "C:\Users\xdohnal\AppData\Local\Programs\pgAdmin 4\v6\runtime\psql.exe" --host "127.0.0.1" --port "61649" --username "pa220" --no-password --dbname "postgres" <"C:\\Homes\\xdohnal\\Downloads\\pa220-data.sql"
-- It takes about 4 hrs to load then.

-- Map CSV with operators (copy the csv file into stratus VM under /var/lib/postgresql/mcc-mnc-table.csv)
CREATE EXTENSION IF NOT EXISTS file_fdw;
CREATE SERVER fwd_file FOREIGN DATA WRAPPER file_fdw;
CREATE FOREIGN TABLE pa220_operators (
	mcc varchar,
	mcc_int varchar,
	mnc varchar,
	mnc_int varchar,
	iso varchar,
	country varchar,
	country_code varchar,
	network varchar
) SERVER fwd_file
OPTIONS ( filename '/var/lib/postgresql/mcc-mnc-table.csv', format 'csv', header 'true' );

--====================================================================================================================--
-- Time dimension
CREATE TABLE time_dimension
(
    time_key        serial PRIMARY KEY,
    time            time UNIQUE NOT NULL,
    second          integer     NOT NULL,
    minute          integer     NOT NULL,
    hour            integer     NOT NULL,
    part_of_the_day varchar     NOT NULL
);

INSERT INTO time_dimension(time, second, minute, hour, part_of_the_day)
SELECT datum::timestamp::time          AS time,
       EXTRACT(second FROM datum)      AS second,
       EXTRACT(minute FROM datum)      AS minute,
       TO_CHAR(datum, 'HH24')::integer AS hour,
       CASE
           WHEN TO_CHAR(datum, 'hh24:mi') BETWEEN '00:00' AND '03:59' THEN 'Night'
           WHEN TO_CHAR(datum, 'hh24:mi') BETWEEN '04:00' AND '07:59' THEN 'Early Morning'
           WHEN TO_CHAR(datum, 'hh24:mi') BETWEEN '08:00' AND '11:59' THEN 'Morning'
           WHEN TO_CHAR(datum, 'hh24:mi') BETWEEN '12:00' AND '15:59' THEN 'Afternoon'
           WHEN TO_CHAR(datum, 'hh24:mi') BETWEEN '16:00' AND '19:59' THEN 'Evening'
           WHEN TO_CHAR(datum, 'hh24:mi') BETWEEN '20:00' AND '23:59' THEN 'Night'
           END                         as part_of_the_day
FROM GENERATE_SERIES('2021-01-01 00:00:00'::timestamp, '2021-01-01 23:59:59'::timestamp, '1 second') datum;

--====================================================================================================================--
-- Date dimension
CREATE TABLE date_dimension
(
    date_key     serial PRIMARY KEY,
    date         date UNIQUE NOT NULL,
    day          integer     NOT NULL,
    day_name     varchar     NOT NULL,
    month        integer     NOT NULL,
    month_name   varchar     NOT NULL,
    year         integer     NOT NULL,

    day_of_week  integer     NOT NULL,
    week_of_year integer     NOT NULL,
    quarter      integer     NOT NULL,
    season       varchar     NOT NULL
);

INSERT INTO date_dimension(date, day, day_name, month, month_name, year, day_of_week, week_of_year, quarter,
                                   season)
SELECT DATE(datum)                 AS date,
       EXTRACT(day FROM datum)     AS day,
       TO_CHAR(datum, 'Day')       AS day_name,
       EXTRACT(month FROM datum)   AS month,
       TO_CHAR(datum, 'Month')     AS month_name,
       EXTRACT(year FROM datum)    AS year,
       EXTRACT(ISODOW FROM datum)  AS day_of_week,
       EXTRACT(WEEK FROM datum)    AS week_of_year,
       EXTRACT(QUARTER FROM datum) AS quarter,

       CASE
           WHEN TO_CHAR(datum, 'MM-DD') BETWEEN '01-01' AND '03-21' THEN 'Winter'
           WHEN TO_CHAR(datum, 'MM-DD') BETWEEN '03-22' AND '06-21' THEN 'Spring'
           WHEN TO_CHAR(datum, 'MM-DD') BETWEEN '06-22' AND '09-21' THEN 'Summer'
           WHEN TO_CHAR(datum, 'MM-DD') BETWEEN '09-22' AND '12-21' THEN 'Autumn'
           WHEN TO_CHAR(datum, 'MM-DD') BETWEEN '12-22' AND '12-31' THEN 'Winter'
           END                     as season
FROM GENERATE_SERIES('2020-01-01 00:00:00'::timestamp, '2022-12-31 23:59:59'::timestamp, '1 day') datum;

INSERT INTO date_dimension (date, day, day_name, month, month_name, year, day_of_week, week_of_year, quarter, season)
VALUES ('1970-01-01', 1, 'N/A', 1, 'N/A', 1970, -1, -1, -1, 'N/A')
ON CONFLICT DO NOTHING;


--====================================================================================================================--
-- GSM network dimension
CREATE TABLE gsm_network_dimension
(
    gsm_key      serial PRIMARY KEY,
    gsmnet_id    varchar NOT NULL UNIQUE,
    country_name varchar NOT NULL,
    network_name varchar NOT NULL
);

INSERT INTO gsm_network_dimension (gsmnet_id, country_name, network_name)
  SELECT mcc||mnc AS gsmnet_id, country, COALESCE(network,'N/A') AS network
  FROM pa220_operators
  ORDER BY gsmnet_id;

INSERT INTO gsm_network_dimension (gsmnet_id, country_name, network_name)
VALUES ('N/A', 'N/A', 'N/A')
ON CONFLICT DO NOTHING;


--====================================================================================================================--
-- Car dimension
CREATE TABLE car_dimension
(
    car_key     serial PRIMARY KEY,
    car_id      integer   NOT NULL,
    make        varchar   NOT NULL,
    spz         varchar   NOT NULL,
    color       varchar   NOT NULL,
    tonnage     real      NOT NULL,

    valid_from  timestamp NOT NULL DEFAULT '2020-01-01 00:00:00',
    valid_to    timestamp NOT NULL DEFAULT '2999-01-01 00:00:00',
    current_row varchar   NOT NULL DEFAULT 'current',
    UNIQUE (car_id, current_row)
);

-- Also trimming peripheral whitespaces and unnecessary symbols from reg plate
-- Value of -1 for tonnage means that the value is missing
INSERT INTO car_dimension(car_id, make, spz, color, tonnage)
SELECT car_key AS car_id,
       CASE
           WHEN make IS NULL OR make = '-------' OR TRIM(make) = '' OR lower(TRIM(make)) = 'null' THEN 'N/A'
           ELSE make END AS make,
       CASE
           WHEN spz IS NULL OR TRIM(spz) = '' OR lower(TRIM(spz)) = 'null' THEN 'N/A'
           ELSE UPPER(TRIM(regexp_replace(spz, '\W+', '', 'g'))) END AS spz,
       CASE
           WHEN color IS NULL OR TRIM(color) = '' OR lower(TRIM(color)) = 'null' THEN 'N/A'
           ELSE UPPER(TRIM(color)) END AS color,
       CASE
           WHEN tonnage IS NULL OR tonnage <= 0 THEN -1
           ELSE tonnage END AS tonnage
FROM public.pa220_car_info;

INSERT INTO car_dimension (car_id, make, spz, color, tonnage)
VALUES (-1, 'N/A', 'N/A', 'N/A', -1)
ON CONFLICT DO NOTHING;

--====================================================================================================================--
-- App dimension
CREATE TABLE app_dimension
(
    app_key     serial PRIMARY KEY,
    program_ver varchar NOT NULL UNIQUE
);

INSERT INTO app_dimension(program_ver)
SELECT DISTINCT CASE
                    WHEN program_ver IS NULL OR TRIM(gsmnet_id) = '' OR lower(TRIM(gsmnet_id)) = 'null' THEN 'N/A'
                    WHEN program_ver = 'A4' THEN 'A04'
                    ELSE program_ver
                    END
FROM public.pa220_data
ORDER BY program_ver;

INSERT INTO app_dimension (program_ver)
VALUES ('N/A')
ON CONFLICT DO NOTHING;


--====================================================================================================================--
-- Simcard dimension
CREATE TABLE simcard_dimension
(
    simcard_key serial PRIMARY KEY,
    sim_imsi    varchar NOT NULL UNIQUE
);

INSERT INTO simcard_dimension(sim_imsi)
SELECT DISTINCT CASE
                    WHEN sim_imsi IS NULL OR TRIM(sim_imsi) = '' OR lower(TRIM(sim_imsi)) = 'null' THEN 'N/A'
                    ELSE sim_imsi END
FROM public.pa220_data;

INSERT INTO simcard_dimension (sim_imsi)
VALUES ('N/A')
ON CONFLICT DO NOTHING;


--====================================================================================================================--
-- Model dimension
CREATE TABLE model_dimension
(
    model_key serial PRIMARY KEY,
    model     varchar NOT NULL UNIQUE
);

INSERT INTO model_dimension(model)
SELECT DISTINCT CASE
                    WHEN device IS NULL OR TRIM(device) = '' OR lower(TRIM(device)) = 'null' THEN 'N/A'
                    ELSE device END
FROM public.pa220_data;

INSERT INTO model_dimension (model)
VALUES ('N/A')
ON CONFLICT DO NOTHING;

--====================================================================================================================--
--====================================================================================================================--

-- Fact table:

--====================================================================================================================--
-- Temporary status_fact table (used to load data from source tables and to associate business ids from source tables
-- with surrogate keys from dimensions)

CREATE TABLE temp_status_fact
(
    car_id        int,
    program_ver   varchar(6),
    timestamp     timestamp,
    gsmnet_id     varchar,
    model         varchar,
    sim_imsi      varchar,

    time_key      int,
    date_key      int,
    car_key       int,
    app_key       int,
    gsm_key       int,
    model_key     int,
    simcard_key   int,
    conn_method   char,
    tracking_mode integer,

    conn_count    integer,
    app_run_time  numeric(6, 2),
    pda_run_time  numeric(10, 2),
    battery_level integer
);


-- Calculate number of connections (conn_count) between each pair of status records. (Query take approximately 80 seconds)
INSERT INTO temp_status_fact (timestamp, car_id, program_ver, gsmnet_id, model, sim_imsi,
                                      conn_method, tracking_mode, app_run_time, pda_run_time, conn_count, battery_level)
	SELECT DATE_TRUNC('minute', timezone('UTC', time_status)) AS timestamp,
	       --time_status,
		MAX(car_key) AS car_id,
                MAX(program_ver) AS program_ver, 
                --MAX(time) AS time,
                MAX(gsmnet_id) AS gsmnet_id,
                MAX(device) AS model,
                sim_imsi,
                CASE
                    WHEN MAX(method) IS NULL OR TRIM(MAX(method)) = ''
                        THEN '-'
                    ELSE MAX(method) END AS conn_method,
                CASE
                    WHEN MAX(tracking_mode) IS NULL OR TRIM(MAX(tracking_mode)) = '' OR lower(TRIM(MAX(tracking_mode))) = 'null'
                        THEN -1
                    ELSE CAST(MAX(tracking_mode) AS INT) END AS tracking_mode,
                MAX(app_run_time) AS app_run_time,
                MAX(pda_run_time) AS pda_run_time,
				MAX(conn_count) AS conn_count,
                CASE
                    WHEN MAX(battery_level) ~ E'^\\d+$'
                        THEN CAST(MAX(battery_level) AS INT)
                    ELSE -1 END AS battery_level
	FROM (SELECT COUNT(*) OVER (partition by sim_imsi, time_status) AS conn_count, X.*
		FROM (SELECT min(time) over (partition by sim_imsi order by sim_imsi,time_conn desc) AS time_status, *
			FROM public.pa220_data
			where time_conn > '2020-01-02 07:30:00' order by sim_imsi,time_conn
		     ) AS X
	     ) AS Y
	GROUP BY sim_imsi, time_status;


-- Update surrogate keys in temporary fact table with corresponding keys in all dimensions (each update takes approximately 30 seconds)
UPDATE temp_status_fact
SET car_key = (SELECT c.car_key
               FROM car_dimension AS c
               WHERE c.car_id = temp_status_fact.car_id
                 AND c.current_row = 'current');

UPDATE temp_status_fact
SET app_key = (SELECT a.app_key
               FROM app_dimension AS a
               WHERE a.program_ver = TRIM(temp_status_fact.program_ver)
                  OR (temp_status_fact.program_ver = 'A4' AND a.program_ver = 'A04'));

UPDATE temp_status_fact
SET gsm_key = (SELECT g.gsm_key
               FROM gsm_network_dimension AS g
               WHERE g.gsmnet_id = TRIM(temp_status_fact.gsmnet_id));

UPDATE temp_status_fact
SET model_key = (SELECT m.model_key
                 FROM model_dimension AS m
                 WHERE m.model = TRIM(temp_status_fact.model));

UPDATE temp_status_fact
SET simcard_key = (SELECT s.simcard_key
                   FROM simcard_dimension AS s
                   WHERE s.sim_imsi = TRIM(temp_status_fact.sim_imsi));

UPDATE temp_status_fact
SET date_key = (SELECT date_key
                FROM date_dimension
                WHERE date = DATE(temp_status_fact.timestamp));

UPDATE temp_status_fact
SET time_key = (SELECT time_key
                FROM time_dimension
                WHERE time = temp_status_fact.timestamp::timestamp::time);


-- Final status_fact table

CREATE TABLE status_fact
(
    time_key      int,
    date_key      int,
    car_key       int,
    app_key       int,
    gsm_key       int,
    model_key     int,
    simcard_key   int,
    conn_method   char,
    tracking_mode integer,

    conn_count    integer,
    app_run_time  numeric(6, 2),
    pda_run_time  numeric(10, 2),
    battery_level integer,

    PRIMARY KEY (time_key, date_key, car_key, app_key, gsm_key, model_key, simcard_key,
                 conn_method, tracking_mode),
    CONSTRAINT fk_time
        FOREIGN KEY (time_key)
            REFERENCES time_dimension (time_key),
    CONSTRAINT fk_date
        FOREIGN KEY (date_key)
            REFERENCES date_dimension (date_key),
    CONSTRAINT fk_car
        FOREIGN KEY (car_key)
            REFERENCES car_dimension (car_key),
    CONSTRAINT fk_app
        FOREIGN KEY (app_key)
            REFERENCES app_dimension (app_key),
    CONSTRAINT fk_gsm
        FOREIGN KEY (gsm_key)
            REFERENCES gsm_network_dimension (gsm_key),
    CONSTRAINT fk_model
        FOREIGN KEY (model_key)
            REFERENCES model_dimension (model_key),
    CONSTRAINT fk_simcard
        FOREIGN KEY (simcard_key)
            REFERENCES simcard_dimension (simcard_key)
);

-- Load data from temporary fact table into final fact table
-- In case when surrogate key is null, surrogate key of corresponding dimension with default value ('N/A','-' or -1 depending on data type) is loaded into final fact table
-- This query takes about 120 seconds.
INSERT INTO status_fact (time_key, date_key, car_key, app_key, gsm_key, model_key, simcard_key,
                                 conn_method, tracking_mode, app_run_time, pda_run_time, battery_level, conn_count)
SELECT DISTINCT ON (time_key, date_key, car_key, app_key, gsm_key, model_key, simcard_key, conn_method, tracking_mode)
	   COALESCE(time_key, (SELECT time_key FROM time_dimension WHERE time = '00:00:00'::timetz)) AS time_key,
       COALESCE(date_key, (SELECT date_key FROM date_dimension WHERE date = '1970-01-01'::date)) AS date_key,
       COALESCE(car_key, (SELECT car_key FROM car_dimension WHERE car_id = -1)),
       COALESCE(app_key, (SELECT app_key FROM app_dimension WHERE program_ver = 'N/A')),
       COALESCE(gsm_key, (SELECT gsm_key FROM gsm_network_dimension WHERE gsmnet_id = 'N/A')),
       COALESCE(model_key, (SELECT model_key FROM model_dimension WHERE model = 'N/A')),
       COALESCE(simcard_key, (SELECT simcard_key FROM simcard_dimension WHERE sim_imsi = 'N/A')),
       conn_method,
       tracking_mode,
       app_run_time,
       pda_run_time,
       battery_level,
       conn_count
FROM temp_status_fact as temp; 


DROP TABLE temp_status_fact;

-- If all succeeds, delete the source data tables to free disk space:
--DROP TABLE public.pa220_data;
--DROP TABLE public.pa220_car_info;

--====================================================================================================================--
-- Credits: Dominik Rumian, 435378; Matej Michálek, 469372; Vlastislav Dohnal, 2952
--====================================================================================================================--

