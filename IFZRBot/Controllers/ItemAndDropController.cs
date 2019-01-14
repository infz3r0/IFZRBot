using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IFZRBot.Models;
using PagedList;

namespace IFZRBot.Controllers
{
    
    public class ItemAndDropController : Controller
    {
        private ifzrtempEntities db = new ifzrtempEntities();

        // GET: ItemAndDrop
        public ActionResult Index(int? page)
        {
            List<item> items = db.items.ToList();

            int pageNum = page ?? 1;
            int pageSize = 10;
            return View(items.ToPagedList(pageNum, pageSize));
        }

        [ChildActionOnly]
        public ActionResult _GetAreaByItem(string itemID)
        {
            List<string> areaids = db.item_drop.Where(x => x.item_id.Equals(itemID)).Select(x=>x.area_id).ToList();
            List<area> areas = db.areas.Where(x => areaids.Contains(x.area_id)).ToList();
            ViewBag.itemID = itemID;
            return PartialView(areas);
        }


        [ChildActionOnly]
        public ActionResult _GetSkillByItemArea(string itemID, string areaID)
        {
            List<item_drop> skills = db.item_drop.Include("skill").Where(x => x.item_id.Equals(itemID) && x.area_id.Equals(areaID)).ToList();

            return PartialView(skills);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            string item_id = form["item_id"];
            string item_name = form["item_name"];
            string item_info = form["item_info"];
            string item_type_id = form["item_type"];

            //Add new item
            item item = new item()
            {
                item_id = item_id,
                item_name = item_name,
                item_info = item_info,
                item_type_id = item_type_id
            };
            //System.Diagnostics.Debug.WriteLine(item.ToString());
            db.items.Add(item);

            int count_area = Convert.ToInt32(form["countarea"]);
            List<string> area_ids;
            int count_skill;
            if (count_area > 0)
            {
                area_ids = new List<string>();
                for (int i=0; i < count_area; i++)
                {
                    string param = "dropdownarea_" + i.ToString();
                    string areai_id = form[param];
                    area_ids.Add(areai_id);
                }
                List<string> distinct = area_ids.Distinct().ToList();
                if (distinct.Count != area_ids.Count)
                {
                    return RedirectToAction("Create");
                }

                for (int i = 0; i < count_area; i++)
                {                               
                    count_skill = Convert.ToInt32(form["countskill_" + i]);
                    if (count_skill > 0)
                    {
                        List<item_drop> item_Drops = new List<item_drop>();
                        List<string> skill_ids = new List<string>();

                        for (int j=0; j < count_skill; j++)
                        {
                            string skill_id = form["dropdownskill_" + i + "_" + j.ToString()];
                            int skill_lv_need = Convert.ToInt32(form["skillLVNeed_" + i + "_" + j.ToString()]);
                            double drop_chance = Convert.ToDouble(form["dropchance_" + i + "_" + j.ToString()]);

                            skill_ids.Add(skill_id);

                            //Add area & skill for item
                            item_drop item_Drop = new item_drop()
                            {
                                item_id = item.item_id,
                                area_id = area_ids[i],
                                skill_id_need = skill_id,
                                skill_level_need = skill_lv_need,
                                item_drop_chance = drop_chance
                            };
                            item_Drops.Add(item_Drop);
                            //System.Diagnostics.Debug.WriteLine(item_Drop.ToString());
                            
                        }

                        if (skill_ids.Count != skill_ids.Distinct().ToList().Count)
                        {
                            return RedirectToAction("Create");
                        }

                        db.item_drop.AddRange(item_Drops);
                    }
                }
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return RedirectToAction("Create");
        }

        public ActionResult _DropDown_ItemType()
        {
            List<Item_type> item_Types = db.Item_type.ToList();
            return PartialView(item_Types);
        }

        public ActionResult _DropDown_Area(string dropdownid)
        {
            List<area> areas = db.areas.ToList();
            ViewBag.dropdownid = dropdownid;
            return PartialView(areas);
        }

        public ActionResult _DropDown_Skill(string dropdownareaid, string dropdownskillid)
        {
            List<skill> skills = db.skills.ToList();
            ViewBag.dropdownareaid = dropdownareaid;
            ViewBag.dropdownskillid = dropdownskillid;
            return PartialView(skills);
        }

    }
}