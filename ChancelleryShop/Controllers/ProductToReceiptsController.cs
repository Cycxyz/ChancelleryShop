using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChancelleryShop;

namespace ChancelleryShop.Controllers
{
    public class ProductToReceiptsController : Controller
    {
        private readonly ChancelleryContext _context;

        public ProductToReceiptsController(ChancelleryContext context)
        {
            _context = context;
        }

        // GET: ProductToReceipts
        public async Task<IActionResult> Index()
        {
            var chancelleryContext = _context.ProductToReceipts.Include(p => p.Product).Include(p => p.Receipt);
            return View(await chancelleryContext.ToListAsync());
        }

        // GET: ProductToReceipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToReceipt = await _context.ProductToReceipts
                .Include(p => p.Product)
                .Include(p => p.Receipt)
                .FirstOrDefaultAsync(m => m.ProductToReceiptId == id);
            if (productToReceipt == null)
            {
                return NotFound();
            }

            return View(productToReceipt);
        }

        // GET: ProductToReceipts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductImage");
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "ReceiptId", "ReceiptId");
            return View();
        }

        // POST: ProductToReceipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductToReceiptId,ReceiptId,ProductId,Amount")] ProductToReceipt productToReceipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productToReceipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductImage", productToReceipt.ProductId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "ReceiptId", "ReceiptId", productToReceipt.ReceiptId);
            return View(productToReceipt);
        }

        // GET: ProductToReceipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToReceipt = await _context.ProductToReceipts.FindAsync(id);
            if (productToReceipt == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductImage", productToReceipt.ProductId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "ReceiptId", "ReceiptId", productToReceipt.ReceiptId);
            return View(productToReceipt);
        }

        // POST: ProductToReceipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductToReceiptId,ReceiptId,ProductId,Amount")] ProductToReceipt productToReceipt)
        {
            if (id != productToReceipt.ProductToReceiptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productToReceipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductToReceiptExists(productToReceipt.ProductToReceiptId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductImage", productToReceipt.ProductId);
            ViewData["ReceiptId"] = new SelectList(_context.Receipts, "ReceiptId", "ReceiptId", productToReceipt.ReceiptId);
            return View(productToReceipt);
        }

        // GET: ProductToReceipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToReceipt = await _context.ProductToReceipts
                .Include(p => p.Product)
                .Include(p => p.Receipt)
                .FirstOrDefaultAsync(m => m.ProductToReceiptId == id);
            if (productToReceipt == null)
            {
                return NotFound();
            }

            return View(productToReceipt);
        }

        // POST: ProductToReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productToReceipt = await _context.ProductToReceipts.FindAsync(id);
            _context.ProductToReceipts.Remove(productToReceipt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductToReceiptExists(int id)
        {
            return _context.ProductToReceipts.Any(e => e.ProductToReceiptId == id);
        }
    }
}
