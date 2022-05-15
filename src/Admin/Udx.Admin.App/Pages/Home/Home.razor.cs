namespace Udx.Admin.App.Pages.Home;
public partial class Home
{

    protected override async Task OnInitializedAsync()
    {
      
        await base.OnInitializedAsync();
    }
   

    public class BasicItem
    {
        public string Title { get; set; }
    }

    public List<BasicItem> data = new()
    {
        new BasicItem { Title = "Udx Design Title 1" },
        new BasicItem { Title = "Udx Design Title 2" },
        new BasicItem { Title = "Udx Design Title 3" },
        new BasicItem { Title = "Udx Design Title 4" },
    };

    public void ItemClick(string title)
    {
        Console.WriteLine($"item was clicked: {title}");
    }
   
}