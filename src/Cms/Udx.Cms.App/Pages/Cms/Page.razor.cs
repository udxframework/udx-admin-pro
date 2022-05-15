namespace Udx.Cms.App.Pages.Cms;

    public partial class Page : ComponentBase
{
        [Inject] public ICmsContentService DataService { get; set; }
        CmsContentResponse _cmsModel;
        [Parameter] public string Id { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _cmsModel = new CmsContentResponse()
            {
                MessageInfo = "没有内容信息~"

            };
            await OnInit(Id);

        }       
        private async  Task OnInit(string id)
        {
           
            try {                
                
                    var request = new DataRequest<string>()
                    {
                        ObjectData = id
                    };
                    var dataResponse = await DataService.GetAsync(request);
                    if (dataResponse.Successful)
                    {
                        if (dataResponse.ObjectData != null)
                        {

                            _cmsModel = dataResponse.ObjectData;
                            _cmsModel.MessageInfo = $"内容信息{System.DateTime.Now}";
                        }
                        else {
                    _cmsModel.Title = "内容不存在~";
                    _cmsModel.MessageInfo = $"内容信息{System.DateTime.Now}";
                }
                    }
                    else {

                        _cmsModel.MessageInfo = dataResponse.Message;
                    }
                
            }
            catch (Exception ex)
            {

                Log.Error("Exception:{@ex}",ex);
                _cmsModel.MessageInfo = ex.Message;
            }
            StateHasChanged();
        }
    }