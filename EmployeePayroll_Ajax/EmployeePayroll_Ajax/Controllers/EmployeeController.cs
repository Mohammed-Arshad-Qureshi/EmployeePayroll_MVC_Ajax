using EmployeePayroll_Ajax.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePayroll_Ajax.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDbContext context;
        public EmployeeController(EmployeeDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                return View(await this.context.Employee.ToListAsync());

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployee(int id=0)
        {
            if(id == 0)
            {
                return View(new EmployeeModel());
            }
            else
            {
                var emp = await this.context.Employee.FindAsync(id);
                if(emp == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(emp);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(int Id, [Bind("Emp_Id,Name,Gender,Department,Notes")] EmployeeModel emps)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (Id == 0)
                {
                    context.Employee.Add(emps);
                    await context.SaveChangesAsync();
                }
                // Update
                else
                {
                    try
                    {
                        context.Employee.Update(emps);
                        await context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EmployeeModelExists(emps.Emp_Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", this.context.Employee.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEmployee", emps) });
        }


        private bool EmployeeModelExists(int id)
        {
            return this.context.Employee.Any(x => x.Emp_Id == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = await this.context.Employee.FirstOrDefaultAsync(x => x.Emp_Id == id);
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emp = await context.Employee.FindAsync(id);
            context.Employee.Remove(emp);
            await context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", context.Employee.ToList()) });
        }

    }

}

