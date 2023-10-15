using Microsoft.AspNetCore.Components;
using TB.Shared.Dto.Global;
using TB.Shared.Dto.User;
using TB.Shared.Enums;
using TB.UI.Services.Repository;

namespace TB.UI.Pages.Dashboard.User
{
    public partial class UserForm
    {
        #region Properties
        [Parameter]
        public UserDto User { get; set; }

        [Parameter]
        public string SubmitTitle { get; set; } = "ذخیره";
        [Parameter]
        public EventCallback OnSubmit { get; set; }
        #endregion

        #region Methods
        private async Task Submit()
        {
            if (OnSubmit.HasDelegate)
            {
                await OnSubmit.InvokeAsync();
            }
        }
        private void OnChangeStatus(ChangeEventArgs args)
        {
            //int.TryParse(args.Value.ToString() , out int val);
            int val = Convert.ToInt32(args.Value);

            User.Status = (StatusType)Enum.ToObject(typeof(StatusType), val);

            //string val = args.Value.ToString();
            //Enum.Parse(typeof(StatusType) , val , true);
        }
        private void OnChangeRole(ChangeEventArgs args)
        {
            int val = Convert.ToInt32(args.Value);

            User.Role = (RoleType)Enum.ToObject(typeof(RoleType), val);
        }
        private void OnConfirmFile(FileDto file)
        {
            User.File = file;
        }
        #endregion
    }
}
