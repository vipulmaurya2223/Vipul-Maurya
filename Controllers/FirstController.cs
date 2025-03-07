using Microsoft.AspNetCore.Mvc;

public class FirstController : Controller
{
    public IActionResult Index1()
    {
        return Content("This is FirstController - Index1");
    }

    public IActionResult Index2()
    {
        return Content("This is FirstController - Index2");
    }

    public IActionResult Index3()
    {
        return Content("This is FirstController - Index3");
    }
}
