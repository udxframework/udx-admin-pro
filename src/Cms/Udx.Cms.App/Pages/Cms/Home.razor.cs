namespace Udx.Cms.App.Pages.Contents;

    public partial class Home : UdxComponentBase
    {
        [Inject] public ICmsContentService DataService { get; set; }
        CmsContentResponse CmsModel;
        [Parameter] public string Id { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            CmsModel = new CmsContentResponse()
            {
                MessageInfo = "没有内容信息~"

            };
            //await OnInit(Id);

        }       
        private async  Task OnInit(string id)
        {
           
            try {                
                
                    var request = new DataRequest<string>()
                    {
                        ObjectData = id,
                        User = User
                    };
                    var dataResponse = await DataService.GetAsync(request);
                    if (dataResponse.Successful)
                    {
                        CmsModel = dataResponse.ObjectData;
                        CmsModel.MessageInfo = $"内容信息{System.DateTime.Now}";
                    }
                    else {

                        CmsModel.MessageInfo = dataResponse.Message;
                    }
                
            }
            catch (Exception ex)
            {

                Log.Error("Exception:{@ex}",ex);
                CmsModel.MessageInfo = ex.Message;
            }
            StateHasChanged();
        }
    }