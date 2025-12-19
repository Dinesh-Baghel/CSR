using Application.Common.Interfaces;
using Dapper;
using Domain.Entities.Request;
using Domain.Entities.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Answer.Command
{
    public class InsertAnswer
    {
        private readonly IDapper _dapper;

        public InsertAnswer(IDapper dapper)
        {
            _dapper = dapper;
        }
        public async Task<AnserSubmitResponse> submitanswer(AnswerRequest request)
        {

           

            AnserSubmitResponse res = new AnserSubmitResponse();
            DataTable answersTable = new DataTable();
            answersTable.Columns.Add("USERNAME", typeof(string));
            answersTable.Columns.Add("LATITUDE", typeof(string));
            answersTable.Columns.Add("LONGITUTDE", typeof(string));
            answersTable.Columns.Add("STORE_CODE", typeof(string));
            answersTable.Columns.Add("QUESTION_ID", typeof(int));
            answersTable.Columns.Add("REMARK_ID", typeof(string));
            answersTable.Columns.Add("IMG_FILE", typeof(string));
            answersTable.Columns.Add("SUBMIT_BY", typeof(string));
            answersTable.Columns.Add("SUBMITED_DATE", typeof(DateTime));
            answersTable.Columns.Add("LASTUPDATED_DATE", typeof(DateTime));
            for (int i = 0; i < request.questionReply.Count; i++)
            {
                string relativePath = "";


                if (request.questionReply[i].imageFile != null)
                {

                    for (int j = 0; j < request.questionReply[i].imageFile.Length; j++)
                    {
                        var imageBytes = Convert.FromBase64String(request.questionReply[i].imageFile[j].ToString());
                        string timestamp = DateTime.Now.ToString("ddMMMyyyyHHmmssfff");
                        string fileName = $"{timestamp}.jpg"; // or use .png based on input
                        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads/");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // Full file path
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderPath, fileName);

                        // Save image to disk
                        await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                        // Return relative path (for frontend)
                        relativePath += "uploads/"+fileName + ",";

                    }


                }
                DateTime parsedDate = DateTime.ParseExact(request.submitDateTime.ToString(), "dd-MMM-yyyy HH:mm:ss fff",
                                          System.Globalization.CultureInfo.InvariantCulture);
                answersTable.Rows.Add(request.userName, request.latitude, request.longitude, request.storeId, request.questionReply[i].questionId, request.questionReply[i].remark, relativePath, request.submitBy, parsedDate, parsedDate);

            }


            var parameters = new DynamicParameters();
            //Input parameters
            parameters.Add("@ANSWER_LIST", answersTable.AsTableValuedParameter("CHECK_LIST_ANSWER_TYPE"));

            var outputParams = new Dictionary<string, DbType>
                {
                    { "@PRO_CODE", DbType.Int32 },
                    { "@PRO_MESSAGE", DbType.String },
                     { "@REF_NO", DbType.String }
                };
            Dictionary<string, string> result = await _dapper.ExecuteStoredProcedureAsync("PRO_INSERT_ANSWER", parameters, outputParams);


            if (result["@PRO_CODE"] == "0")
            {
                res.responseCode = 0;
                res.responseMessage = result["@PRO_MESSAGE"];
                res.referenceNumber = result["@REF_NO"];
            }
            else
            {
                res.responseCode = Convert.ToInt32(result["@PRO_CODE"]);
                res.responseMessage = result["@PRO_MESSAGE"];
            }

            return res;
        }



    }
}
