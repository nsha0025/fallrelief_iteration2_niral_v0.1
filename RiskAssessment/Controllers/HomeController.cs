using RiskAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using HiQPdf;

namespace RiskAssessment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult testflaticon()
        {
            return View();

        }
        public ActionResult Index()
        {
            return View();
           
        }



        public string RenderViewAsString(string viewName, object model)
        {
            // create a string writer to receive the HTML code
            StringWriter stringWriter = new StringWriter();

            // get the view to render
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
            // create a context to render a view based on a model
            ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    new ViewDataDictionary(model),
                    new TempDataDictionary(),
                    stringWriter
                    );

            // render the view to a HTML code
            viewResult.View.Render(viewContext, stringWriter);

            // return the HTML code
            return stringWriter.ToString();
        }


        [HttpPost]
        public ActionResult ConvertThisPageToPdf()
        {
            // get the HTML code of this view
            string htmlToConvert = RenderViewAsString("Index", null);

            // the base URL to resolve relative images and css
            String thisPageUrl = this.ControllerContext.HttpContext.Request.Url.AbsoluteUri;
            String baseUrl = thisPageUrl.Substring(0, thisPageUrl.Length - "Home/ConvertThisPageToPdf".Length);

            // instantiate the HiQPdf HTML to PDF converter
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            // hide the button in the created PDF
            htmlToPdfConverter.HiddenHtmlElements = new string[] { "#convertThisPageButtonDiv" };

            // render the HTML code as PDF in memory
            byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, baseUrl);

            // send the PDF file to browser
            FileResult fileResult = new FileContentResult(pdfBuffer, "application/pdf");
            fileResult.FileDownloadName = "ThisMvcViewToPdf.pdf";

            return fileResult;
        }













        /*
        
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {

                
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
                


                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();

                return File(stream.ToArray(), "application/pdf", "Grid.pdf");



            }
        }
        

        
 
        
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            this.Page.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
        }

        






        protected void ExportToPDF(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    // GridView1.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }

        */

















        /*

        protected override void Render(HtmlTextWriter writer)
        {
            MemoryStream mem = new MemoryStream();
            StreamWriter twr = new StreamWriter(mem);
            HtmlTextWriter myWriter = new HtmlTextWriter(twr);
            base.Render(myWriter);
            myWriter.Flush();
            myWriter.Dispose();
            StreamReader strmRdr = new StreamReader(mem);
            strmRdr.BaseStream.Position = 0;
            string pageContent = strmRdr.ReadToEnd();
            strmRdr.Dispose();
            mem.Dispose();
            writer.Write(pageContent);
            CreatePDFDocument(pageContent);
        }
        public void CreatePDFDocument(string strHtml)
        {

            string strFileName = HttpContext.Current.Server.MapPath("test.pdf");
            // step 1: creation of a document-object
            Document document = new Document();
            // step 2:
            // we create a writer that listens to the document
            PdfWriter.GetInstance(document, new FileStream(strFileName, FileMode.Create));
            StringReader se = new StringReader(strHtml);
            HTMLWorker obj = new HTMLWorker(document);
            document.Open();
            obj.Parse(se);
            document.Close();
            ShowPdf(strFileName);

        }
        public void ShowPdf(string strFileName)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
            Response.ContentType = "application/pdf";
            Response.WriteFile(strFileName);
            Response.Flush();
            Response.Clear();
        }

        */













        //    private void Button1_click(object sender, System.EventArgs e)
        //    {
        //       Response.ContentType = "Application/pdf";
        //     Response.AppendHeader("Content-Disposition", "attachment; filename=help.pdf");
        //        Response.TransmitFile(Server.MapPath("~/doc/help.pdf"));
        //        Response.End();
        //   }




        public ActionResult Assessment()
        {
            var _ctx = new fall_reliefEntities();
            ViewBag.Assessment = _ctx.tbl_RiskAss_Assessment.Where(x => x.IsActive == true).Select(x => new { x.AssessmentTypeID, x.AssessmentType }).ToList();

            SessionModel _model = null;

            if (Session["SessionModel"] == null)
                _model = new SessionModel();
            else
                _model = (SessionModel)Session["SessionModel"];


            return View(_model);

        }

        public ActionResult Instruction(SessionModel model)
        {

            if (model != null) 
            {
                var _ctx = new fall_reliefEntities();
                var assessment = _ctx.tbl_RiskAss_Assessment.Where(x => x.AssessmentTypeID == model.AssessmentTypeID).FirstOrDefault();
                if (assessment != null) 
                {
                    ViewBag.AssessmentType = assessment.AssessmentType;
                    ViewBag.AssessmentDescription = assessment.Description;
                
                }
            }
                Session["SessionModel"] = model;

            if (model == null)   
                return RedirectToAction("Assessment");
            

            return View (model);
        }

        public ActionResult RiskAssessment(SessionModel model)
        {
            if (model != null)
            {
                Session["SessionModel"] = model;
            }

            if (model == null)
            {
                return RedirectToAction("Assessment");
            }

            var _ctx = new fall_reliefEntities();
            //To create new assessment
            tbl_RiskAss_Session newAssessment = new tbl_RiskAss_Session()
            {
                timeStamp = DateTime.UtcNow,
                AssessmentTypeID = model.AssessmentTypeID,
                sessionID = Guid.NewGuid()

            };
            _ctx.tbl_RiskAss_Session.Add(newAssessment);
            _ctx.SaveChanges();

            this.Session["SessionID"] = newAssessment.sessionID;

            return RedirectToAction("QuestionAssessment", new { @SessionID = Session["SessionID"]});
        }


        public ActionResult QuestionAssessment(Guid SessionID, int? qno)
        {

            if (SessionID == null)
            {
                return RedirectToAction("Assessment");

            }

            var _ctx = new fall_reliefEntities();

            var asessment = _ctx.tbl_RiskAss_Session.Where(x => x.sessionID.Equals(SessionID)).FirstOrDefault();

            if (asessment == null)
            {
                return RedirectToAction("Assessment");
            }

            if (qno.GetValueOrDefault() < 1)
                qno = 1;

            var assQuestionID = _ctx.tbl_RiskAss_AssQuestion
                .Where(x => x.AssessmentTypeID == asessment.AssessmentTypeID && x.QuestionNumber == qno)
                .Select(x => x.ID).FirstOrDefault();



            if (assQuestionID > 0)
            {
                var _model = _ctx.tbl_RiskAss_AssQuestion.Where(x => x.ID == assQuestionID)
                    .Select(x => new QuestionModel()
                    {

                        Question = x.tbl_RiskAss_Questions.Question,
                        QuestionNumber = x.QuestionNumber,
                        QuestionType = x.tbl_RiskAss_Questions.AnswerType,
                        AssessmentTypeID = x.AssessmentTypeID,
                        AssessmentType = x.tbl_RiskAss_Assessment.AssessmentType,
                        QuestionSection = x.tbl_RiskAss_Questions.QuestionSection,
                        Options = x.tbl_RiskAss_Questions.tbl_RiskAss_ResponseChoice.Select(y => new QRmodel()
                        {
                            ResponseID = y.ID,
                            Response = y.Response
                        }).ToList()
                    }).FirstOrDefault();


                //now if it is already answered ealier, set the choice of the user
                var savedAnswers = _ctx.tbl_RiskAss_AssessmentResponse.Where(x => x.AssQuestionID == assQuestionID && x.AssessmentNo == asessment.AssessmentNo)
                    .Select(x => new { x.responseID, x.Answer }).ToList();

                foreach (var savedAnswer in savedAnswers)
                {
                    _model.Options.Where(x => x.ResponseID == savedAnswer.responseID).FirstOrDefault().Answer = savedAnswer.Answer;
                }

                _model.TotalQuestionNo = _ctx.tbl_RiskAss_AssQuestion.Where(x => x.AssessmentTypeID == asessment.AssessmentTypeID).Count();

                return View(_model);
            }

            else
            {

                return View("Error");
            }

        }
        [HttpPost]
        public ActionResult PostAnswer(AnswerModel repsonses)
        {
            var _ctx = new fall_reliefEntities();
            var assessment = _ctx.tbl_RiskAss_Session.Where(x => x.sessionID.Equals(repsonses.SessionID)).FirstOrDefault();

            var AssQuestionInfo = _ctx.tbl_RiskAss_AssQuestion.Where(x => x.AssessmentTypeID == assessment.AssessmentTypeID && x.QuestionNumber == repsonses.QuestionID)
                .Select(x => new
                {
                    QID = x.QuestionID,
                    AssQuestionID = x.ID,
                    QType = x.tbl_RiskAss_Questions.AnswerType,
                    QSection = x.tbl_RiskAss_Questions.QuestionSection,
                    Rscore = x.tbl_RiskAss_Questions.RiskScore
                }).FirstOrDefault();

            var existingRecord = _ctx.tbl_RiskAss_AssessmentResponse.Where(x => x.AssessmentNo == assessment.AssessmentNo && x.AssQuestionID == AssQuestionInfo.AssQuestionID).FirstOrDefault();

            //insert reponses into database
            if (existingRecord == null)
            {
                var ReponseResult = (
                 from a in _ctx.tbl_RiskAss_ResponseChoice
                 join b in repsonses.UserSelectedID on a.ID equals b
                 select new { a.ID, a.RiskScore }).AsEnumerable()
                 .Select(x => new tbl_RiskAss_AssessmentResponse()
                 {
                     id = Guid.NewGuid(),
                     AssessmentNo = assessment.AssessmentNo,
                     AssQuestionID = AssQuestionInfo.AssQuestionID,
                     responseID = x.ID,
                     Answer = "Checked"
                 }).ToList();

                _ctx.tbl_RiskAss_AssessmentResponse.AddRange(ReponseResult);
                _ctx.SaveChanges();
            }

            //update existing record
            else
            {
                var dbRecord = _ctx.tbl_RiskAss_AssessmentResponse.Find(existingRecord.id);

                //need to check this

                var ReponseResult = (
                 from a in _ctx.tbl_RiskAss_ResponseChoice
                 join b in repsonses.UserSelectedID on a.ID equals b
                 select new { a.ID, a.RiskScore }).AsEnumerable()
                 .Select(x => new tbl_RiskAss_AssessmentResponse()
                 {
                     responseID = x.ID,
                     Answer = "Checked"
                 }).FirstOrDefault();

                dbRecord.responseID = ReponseResult.responseID;
                dbRecord.Answer = ReponseResult.Answer;

                _ctx.SaveChanges();

            }

            //get the next question depending on the direction
            var nextQuestionNumber = 1;

            if (repsonses.Direction.Equals("forward", StringComparison.CurrentCultureIgnoreCase))
            {
                nextQuestionNumber = _ctx.tbl_RiskAss_AssQuestion.Where(x => x.AssessmentTypeID == repsonses.AssessmentTypeID
                && x.QuestionNumber > repsonses.QuestionID).OrderBy(x => x.QuestionNumber).Take(1).Select(x => x.QuestionNumber).FirstOrDefault();

            }
            else
            {
                nextQuestionNumber = _ctx.tbl_RiskAss_AssQuestion.Where(x => x.AssessmentTypeID == repsonses.AssessmentTypeID
                && x.QuestionNumber < repsonses.QuestionID).OrderByDescending(x => x.QuestionNumber).Take(1).Select(x => x.QuestionNumber).FirstOrDefault();

            }

            if (nextQuestionNumber < 1)
                nextQuestionNumber = 1;

            return RedirectToAction("QuestionAssessment", new
            {
                @SessionID = Session["SessionID"],
                @qno = nextQuestionNumber
            });

        }



    }
}