using BL.DTOs.User;

namespace FE.Models.Admin
{
    public class AdminUserViewModel
    {
        public IEnumerable<UserDetailDto> Users { get; set; }
        public PaginationViewModel Pagination { get; set; }

    }
}
