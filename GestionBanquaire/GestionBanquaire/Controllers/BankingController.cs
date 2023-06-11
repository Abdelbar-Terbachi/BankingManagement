using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionBanquaire.Models;

namespace GestionBanquaire.Controllers
{
    public class BankingController : Controller
    {
        private readonly BankingContext _context;

        public BankingController(BankingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View("Index", users);
        }

        [HttpPost]
        public IActionResult Index(int userId, string transactionType, decimal amount)
        {
            if (transactionType == "Debit" || transactionType == "Credit")
            {
                return RedirectToAction("Invoice", new { userId = userId, transactionType = transactionType, amount = amount });
            }
            else
            {
                return RedirectToAction("Transaction", new { id = userId });
            }
        }

        [HttpGet]
        public IActionResult Transaction(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var invoiceViewModel = new InvoiceViewModel
            {
                UserName = user.Username,
            };

            var transactionViewModel = new TransactionViewModel
            {
                User = user,
                Invoice = invoiceViewModel
            };

            return View(transactionViewModel);
        }

        [HttpPost]
        public IActionResult Transaction(TransactionViewModel transactionViewModel, string transactionType, decimal amount)
        {
            var userId = transactionViewModel.User?.Id;
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            if (transactionType == "Credit")
            {
                user.Balance += amount;
            }
            else if (transactionType == "Debit")
            {
                if (user.Balance >= amount)
                {
                    user.Balance -= amount;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Insufficient balance.");
                    ViewBag.User = user;
                    return View("Transaction");
                }
            }
            else if (transactionType == "Transfer")
            {
                var recipient = _context.Users.FirstOrDefault(u => u.Id == transactionViewModel.RecipientId);
                if (recipient == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid recipient account number.");
                    ViewBag.User = user;
                    return View("Transaction");
                }

                if (user.Balance >= amount)
                {
                    user.Balance -= amount;
                    recipient.Balance += amount;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Insufficient balance.");
                    ViewBag.User = user;
                    return View("Transaction");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid transaction type.");
                ViewBag.User = user;
                return View("Transaction");
            }

            _context.SaveChanges();

            return RedirectToAction("Invoice", new { userId = userId, transactionType = transactionViewModel.Invoice.TransactionType, amount = transactionViewModel.Amount });
        }

        [HttpGet]
        public IActionResult Invoice(int userId, string transactionType, decimal amount)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            decimal currentBalance = user.Balance;

            var invoice = new InvoiceViewModel
            {
                UserName = user.Username,
                TransactionType = transactionType,
                CurrentBalance = currentBalance,
                TransactionAmount = amount,
                TransactionId = Guid.NewGuid(),
                DateTime = DateTime.Now
            };

            return View("Invoice", invoice);
        }
    }
}
