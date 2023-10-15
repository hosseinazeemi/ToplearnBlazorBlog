using Microsoft.AspNetCore.Components;
using TB.Shared.Dto.Category;
using TB.Shared.Enums;

namespace TB.UI.Pages.Dashboard.Category
{
    public partial class CategoryForm
    {
        #region Properties
        [Parameter]
        public CategoryDto Category { get; set; }

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
        private void OnIsSpecialChange(ChangeEventArgs args)
        {
            bool val = Convert.ToBoolean(args.Value);

            if (val)
            {
                Category.IsSpecial = true;
            }
            else
            {
                Category.IsSpecial = false;
            }
        }
        private void OnChangeStatus(ChangeEventArgs args)
        {
            //int.TryParse(args.Value.ToString() , out int val);
            int val = Convert.ToInt32(args.Value);

            Category.Status = (StatusType)Enum.ToObject(typeof(StatusType), val);

            //string val = args.Value.ToString();
            //Enum.Parse(typeof(StatusType) , val , true);
        }
        #endregion
    }
}
