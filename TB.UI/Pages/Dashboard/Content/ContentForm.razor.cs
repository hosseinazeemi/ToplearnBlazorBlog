using Microsoft.AspNetCore.Components;
using TB.Shared.Dto.Category;
using TB.Shared.Dto.Content;
using TB.Shared.Dto.Global;
using TB.Shared.Enums;
using TB.UI.Services.Repository;

namespace TB.UI.Pages.Dashboard.Content
{
    public  partial class ContentForm
    {
        #region Properties
        [Parameter]
        public ContentDto Content { get; set; }

        [Parameter]
        public string SubmitTitle { get; set; } = "ذخیره";
        [Parameter]
        public EventCallback OnSubmit { get; set; }

        [Inject]
        private ICategoryService _categoryService { get; set; }
        private List<CategoryDto> categories = new List<CategoryDto>();

        #endregion

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            //categories.Add(new CategoryDto
            //{
            //    Id = 0,
            //    Name = "لطفا انتخاب کنید"
            //});
            var response = await _categoryService.GetAll();
            if (response.Status)
            {
                categories = response.Data;
            }
            await base.OnInitializedAsync();
        }
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

            Content.Status = (StatusType)Enum.ToObject(typeof(StatusType), val);

            //string val = args.Value.ToString();
            //Enum.Parse(typeof(StatusType) , val , true);
        }
        private void OnChangeContentType(ChangeEventArgs args)
        {
            int val = Convert.ToInt32(args.Value);

            Content.Type = (ContentType)Enum.ToObject(typeof(ContentType), val);
        }
        private void OnConfirmFile(FileDto file)
        {
            Content.File = file;
        }

        private void OnChangeCategory(ChangeEventArgs args)
        {
            Content.CategoryId = Convert.ToInt32(args.Value);
        }
        #endregion
    }
}
