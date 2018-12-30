using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {


        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        public IActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();

            return View(indexViewModel);
        }

        public IActionResult Error()
        {

            return View();
        }

        public IActionResult Result()
        {
            ResultViewModel resultViewModel = new ResultViewModel();

            resultViewModel.Error = "To add a new shape, please return to the 'Add' page.";

            List<Shape> TheList = context.Shapes.ToList();
            resultViewModel.Shapelist = TheList;

            return View(resultViewModel);
        }

        [HttpPost]
        public IActionResult Result(ResultViewModel resultViewModel)

        {
            if (ModelState.IsValid)
            {

                List<Shape> TheList = context.Shapes.ToList();

                if (resultViewModel.Shapetype == "Cube")
                {

                    Cube Cube = new Cube("Cube", resultViewModel.Sidelength);

                    resultViewModel.Volume = Cube.Volume(resultViewModel.Sidelength);
                    resultViewModel.Surfacearea = Cube.Surfacearea(resultViewModel.Sidelength);

                    context.Shapes.Add(Cube);
                    TheList.Add(Cube);

                }

                if (resultViewModel.Shapetype == "Square")
                {

                    Square Square = new Square("Square", resultViewModel.Sidelength);

                    resultViewModel.Perimeter = Square.Perimeter(resultViewModel.Sidelength);
                    resultViewModel.Area = Square.Area(resultViewModel.Sidelength);
                    context.Shapes.Add(Square);
                    TheList.Add(Square);

                }

                if (resultViewModel.Shapetype == "Segment")
                {

                    Segment Segment = new Segment("Segment", resultViewModel.Sidelength);
                    context.Shapes.Add(Segment);
                    TheList.Add(Segment);

                }

                context.SaveChanges();
                resultViewModel.Shapelist = TheList;

                return View(resultViewModel);


            }


            return Redirect("/Home/Error");

        }
    }

}