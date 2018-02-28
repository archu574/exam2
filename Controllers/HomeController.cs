using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using exam2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace exam2.Controllers
{
    public class HomeController : Controller
    {
        private ExamContext _context;
 
        public HomeController(ExamContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            
            return View("index");
        }

        [HttpPost]
        [Route("submit")]
        public IActionResult submit(string firstname,string lastname,string username,string pwd,string cpwd)
        {


            User a=new User();
            a.firstname=firstname;
            a.lastname=lastname;
            a.username=username;
            a.password=pwd;
            a.passwordconfirmation=cpwd;
            a.wallet=1000;

            TryValidateModel(a);
            
            // TempData["err"]=ModelState.Values;

            

            if(ModelState.IsValid){
                
            List<User> CheckEmail = _context.User.Where(theuser => theuser.username == username).ToList();
                if (CheckEmail.Count > 0)
                    {
                        ViewBag.ErrorRegister = "UserName already in use...";
                        return View("Index");
                    }
            this._context.User.Add(a);
            this._context.SaveChanges();

            User userobj = _context.User.SingleOrDefault(User => User.username == username);

            HttpContext.Session.SetInt32("UserId", (int)userobj.id);
            HttpContext.Session.SetString("UserName", (string)userobj.firstname );
            return RedirectToAction("dashboardpage");
            

            }
                
            else{
                TempData["err"]=ModelState.Values;
                return View("index");
                
            }

            
        }

        

        [HttpGet]
        [Route("login")]
        public IActionResult login()
        {
            
            return View("login");
        }

        [HttpPost]
        [Route("loginsubmit")]
        public IActionResult loginsubmit(string username,string password)
        {   
            if(ModelState.IsValid)
            { 
               User userobj = _context.User.SingleOrDefault(User => User.username == username);
            if(userobj == null)
            {
                ViewBag.error = "Invalid Credentials";
                return View("login");
            }
             if((string)userobj.username == username && (string)userobj.password == password)
            {
                // Account LoggedUserAccount = _context.User.Single(theAccount => theAccount.id == userobj.id);
                HttpContext.Session.SetInt32("UserId", (int)userobj.id);
                HttpContext.Session.SetString("UserName", (string)userobj.firstname );
                return RedirectToAction("dashboardpage");
            }
                ViewBag.error = "Invalid Credentials";
                return View("login");
            }

           return View("login"); 
            
        }

        [HttpGet]
        [Route("newuser")]
        public IActionResult newuser()
        {
             
            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login");
        }

        [HttpGet]
        [Route("home")]
        public IActionResult home()
        {
            
            return RedirectToAction("dashboardpage");
        }

        [HttpGet]
        [Route("dashboardpage")]
        public IActionResult dashboardpage()
        {
           
            
            int  USERID = (int) HttpContext.Session.GetInt32("UserId");

           
            User Userz= _context.User.Include(user => user.auctionsbyusers).SingleOrDefault(i =>i.id==USERID);
             var y = _context.Auction.Include(u =>u.userslist).ThenInclude(a =>a.User).ToList();

            //  List<UserhasActivity> userhasactlist = _context.User_has_Activity.ToList();
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            User currentuser = _context.User.SingleOrDefault(i=> i.id==USERID);
            List<Auction> allauctions = _context.Auction.ToList();

         
            ViewBag.currentwallet=currentuser;
            ViewBag.allauctions=allauctions;
            
            
            // return View("dashboardpage",y);
            return View("dashboardpage",y);
        }

        [HttpGet]
        [Route("newauctionpage")]
        public IActionResult newauctionpage()
        {
            ViewBag.errors = new List <string>();
            return View("newauctionpage");
        }

        [HttpPost]
        [Route("addingauction")]
        public IActionResult addingauction(Auction model)
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            User newuser = _context.User.SingleOrDefault(i => i.id==id);
            if(ModelState.IsValid)
            {
                int uId = Convert.ToInt32 (id);
                Auction newauction = new Auction()
                {
                    productname = model.productname,
                    createdby = newuser.firstname,
                    description= model.description,
                    startingbid =model.startingbid,
                    enddate=model.enddate,
                    
                    
                };
                _context.Auction.Add(newauction);
                _context.SaveChanges();
                return RedirectToAction ("dashboardpage");
            }
            else {
                return View ("NewActivity");
            }
        }

        [HttpGet]
        [Route("deleteauction/{auctionid}")]
        public IActionResult delete(int auctionid)
        {
            
            Auction auction = _context.Auction.Where(w => w.id == auctionid).SingleOrDefault();
           
            _context.Auction.Remove(auction);
            _context.SaveChanges();
            return RedirectToAction("dashboardpage");
        }

        [HttpGet]
        [Route("bidpage/{auctionid}")]
        public IActionResult bidpage(int auctionid)
        {
             ViewBag.UserName = HttpContext.Session.GetString("UserName");
            Auction auction = _context.Auction.Where(w => w.id == auctionid).SingleOrDefault();
           ViewBag.thatauction=auction;
            
            return View("bidpage");
        }

        [HttpPost]
        [Route("bidsubmit/{auctionid}")]
        public IActionResult bidsubmit(int auctionid,int bidnumber)
        {
            
            Auction auction = _context.Auction.Where(w => w.id == auctionid).SingleOrDefault();
            var userid = HttpContext.Session.GetInt32("UserId");
            User bidguy = _context.User.Where(w => w.id == userid).SingleOrDefault();
            User Userz= _context.User.Include(user => user.auctionsbyusers).SingleOrDefault(i =>i.id==auctionid);
            Auction auc= _context.Auction.Include(au => au.userslist).SingleOrDefault(i =>i.id==userid);
            UserhasAuction userhasauc = _context.User_has_Auction.Where(w => w.UserId == userid).SingleOrDefault();

            // Auction auctiontopbid=_context.Auction.Where(w=>w.id == auctionid).SingleOrDefault(a=> a.topbid)

                
                if (auction.topbid>=0)
                {
                    if(auction.topbid<bidnumber){
                        auction.topbid = bidnumber ;
                        if(bidguy.wallet>bidnumber){
                            bidguy.wallet-=bidnumber;
                        
                        }
                        this._context.Auction.Attach(auction);
                        this._context.SaveChanges();
                        this._context.User.Attach(bidguy);
                        this._context.SaveChanges();
                    }
                   
                }
            
            
            return RedirectToAction("dashboardpage");
        }



       
    }
}



       
    
        
      

        
        
  
