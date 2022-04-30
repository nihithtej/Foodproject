﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcFood.Data;
using MvcFood.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace MvcFood.Controllers
{
    public class FoodsController : Controller
    {
        private readonly MvcFoodContext _context;

        public FoodsController(MvcFoodContext context)
        {
            _context = context;
        }
        HttpClient httpClient;

        static string BASE_URL = "https://api.nal.usda.gov/fdc/v1/";
        static string API_KEY = "ZjvbCsuwLqg3LGCJJblZtswa3N2wrPAB5tu6WEjg";

        public IActionResult Index(string searchString)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string FOOD_NUTRIITION_PATH = BASE_URL + "/search?query=%22%2B" + searchString + "%22&dataType=Foundation";
            string nutritionData = "";

            Foodresults foodresults = null;


            httpClient.BaseAddress = new Uri(FOOD_NUTRIITION_PATH);
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(FOOD_NUTRIITION_PATH).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    nutritionData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                }

                if (!nutritionData.Equals(""))
                {
                    foodresults = JsonConvert.DeserializeObject<Foodresults>(nutritionData);
                    foodresults = JsonConvert.DeserializeObject<Foodresults>(nutritionData);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //foreach(Foodresults item in foodresults)
            //Food food1 = new Food();

            //food1.description = foodresults.foodsapi.;

            //context.Food.Add(food1);
            //context.SaveChanges();

            //Nutrient nutrient1 = new Nutrient();
            //nutrient1.nutrientName = "Carbohydrates";
            //nutrient1.nutrientNumber = 1;
            //context.Nutrient.Add(nutrient1);
            //context.SaveChanges();

            //Food_Nutrient fn1 = new Food_Nutrient();
            //fn1.value = 1;
            //fn1.unitName = "mg";
            //fn1.food = food1;
            //fn1.nutrient = nutrient1;
            //context.Food_Nutrient.Add(fn1);
            //context.SaveChanges();

            //;
            return View(foodresults);
        }

        // GET: Foods 
        public async Task<IActionResult> Display(string searchString)
        {
            var foods = from f in _context.Food
                        select f;

            if (!String.IsNullOrEmpty(searchString))
            {
                foods = foods.Where(s => s.description.Contains(searchString));
            }
            var v = foods.ToListAsync();
            return View(await v);
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var foodnutrients = from f in _context.Food_Nutrient
                                join m in _context.Nutrient on f.nutrient.nutrientId equals m.nutrientId
                                where f.food.fdcId == id
                                select new FoodNutrients { foodvm = f, nutrientvm = m };
            //var food = await _context.Food
            // .FirstOrDefaultAsync(m => m.fdcId == id);
            //n.value=nutrient.valu

            //List<String> nut = (from f in _context.Food_Nutrient
            //          join m in _context.Nutrient on f.nutrient.nutrientId equals m.nutrientId
            //          where f.food.fdcId == id
            //          select new { nutrientcol = m.nutrientName }).ToList();



            if (foodnutrients == null)
            {
                return NotFound();
            }

            var nut = foodnutrients.ToList();
            //var l = new List<String>();
            string s = "[";

            foreach (FoodNutrients item in nut)
            {
                //l.Add(item.nutrientvm.nutrientName);
                s = s + "\"" + item.nutrientvm.nutrientName + "\",";
            }
            s = s.Remove(s.Length - 1);
            s = s + "]";
            //s = "[\"Carb\",\"Fat\"]";

            string s2 = "[";

            foreach (FoodNutrients item in nut)
            {
                //l.Add(item.nutrientvm.nutrientName);
                s2 = s2 + item.foodvm.value + ",";
            }
            s2 = s2.Remove(s2.Length - 1);
            s2 = s2 + "]";


            //var l = nut[0];
            //String h = l.nutrientvm.nutrientName;
            //k = 1;
            var model = new Viewmodel();
            model.foodnutrients = foodnutrients;
            model.str = s;
            model.num = s2;
            //foreach (FoodNutrients item2 in model.foodnutrients)
            //{
            //    var q = item2.foodvm.food;
            //}

            return View(model);
        }


        // GET: Foods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("fdcId,description")] Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("fdcId,description")] Food food)
        {
            if (id != food.fdcId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.fdcId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Food
                .FirstOrDefaultAsync(m => m.fdcId == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            List<int> fn_ids = _context.Food_Nutrient.Where(x => x.food.fdcId == id).Select(x => x.FNId).ToList();
            foreach (int i in fn_ids)
            {
                var fn = await _context.Food_Nutrient.FindAsync(i);
                _context.Food_Nutrient.Remove(fn);
            }

            var food = await _context.Food.FindAsync(id);
            _context.Food.Remove(food);


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return _context.Food.Any(e => e.fdcId == id);
        }
    }
}







