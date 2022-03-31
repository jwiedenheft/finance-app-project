using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers;

public class TransactionsController : Controller
{

    private readonly FinanceAppDbContext _db;
    private decimal balance = 0;

    public TransactionsController(FinanceAppDbContext db)
    {
        _db = db;
        balance = CalcBalance();
    }

    private decimal CalcBalance(){
        decimal sum = 0;
        foreach (Transaction transaction in _db.Transactions){
            sum += transaction.Amount;
        }
        Console.WriteLine(sum);
        return sum;
    }

    public IActionResult Index()
    {
        IEnumerable<Transaction> transactionList = _db.Transactions.ToList();

        return View(transactionList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Transaction transactionObj)
    {
        if (ModelState.IsValid)
        {
            _db.Transactions.Add(transactionObj);
            _db.SaveChanges();
            CalcBalance();
            TempData["success"] = $"Added \"{transactionObj.Name}\"!";
            return RedirectToAction("Index");
        }
        return View(transactionObj);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0) return NotFound();
        var transaction = _db.Transactions.Find(id);

        if (transaction == null) return NotFound();

        return View(transaction);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Transaction transactionObj)
    {
        if(ModelState.IsValid){
            _db.Transactions.Update(transactionObj);
            _db.SaveChanges();
            CalcBalance();
            TempData["success"] = $"\"{transactionObj.Name}\" successfully updated!";
            return RedirectToAction("Index");
        }
        return View(transactionObj);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0 ) return NotFound();
        var transaction = _db.Transactions.Find(id);
        if (transaction == null) return NotFound();
        return View(transaction);
    }

    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        if (id == null || id == 0 ) return NotFound();
        var transaction = _db.Transactions.Find(id);
        if (transaction == null) return NotFound();
        _db.Transactions.Remove(transaction);
        _db.SaveChanges();
        CalcBalance();
        TempData["success"] = $"\"{transaction.Name}\" deleted.";
        return RedirectToAction("Index");
    }


}