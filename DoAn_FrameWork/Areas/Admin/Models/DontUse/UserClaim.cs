using System;
using System.Collections.Generic;

namespace DoAn_FrameWork.Areas.Admin.Models.DontUse
{
    public partial class UserClaim
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
    }
}
