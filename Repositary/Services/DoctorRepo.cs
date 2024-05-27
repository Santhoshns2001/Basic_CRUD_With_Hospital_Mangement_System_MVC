﻿using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class DoctorRepo :IDoctorRepo
    {

       private readonly SqlConnection conn = new SqlConnection();

        private readonly string sqlConnectionString;

        private readonly IConfiguration configuration;

        public DoctorRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
            sqlConnectionString = configuration.GetConnectionString("DbConnection");
            conn.ConnectionString = sqlConnectionString;
        }


        public bool RegisterDoc(DoctorModel doctorModel)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("usp_RegisterDoc", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@fullname", doctorModel.FullName);
                cmd.Parameters.AddWithValue("@email", doctorModel.Email);
                cmd.Parameters.AddWithValue("@contact", doctorModel.Contact);
                cmd.Parameters.AddWithValue("@address", doctorModel.Address);
                cmd.Parameters.AddWithValue("@dob", doctorModel.DOB);
                cmd.Parameters.AddWithValue("@gender", doctorModel.Gender);
                cmd.Parameters.AddWithValue("@qualification", doctorModel.Qualification);
                cmd.Parameters.AddWithValue("@specailization", doctorModel.Specailization);
                cmd.Parameters.AddWithValue("@Experience", doctorModel.Experience);
                cmd.Parameters.AddWithValue("@doctorimage", doctorModel.DoctorImage);
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                conn.Close();
               
            }
            return false;
        }

        public List<DoctorModel> FetchAllDocs()
        {

            List<DoctorModel> doctors = new List<DoctorModel>();
            try
            {
                if (conn != null)
                {

                    SqlCommand cmd = new SqlCommand("usp_FetchAllDoctors", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        DoctorModel doctorModel = new DoctorModel()
                        {
                            DoctorId = (int)sqlDataReader["Doctor_Id"],
                            FullName = (string)sqlDataReader["fullName"],
                            Email = (string)sqlDataReader["Email"],
                            Contact = (long)sqlDataReader["Contact"],
                            Address = (string)sqlDataReader["Address"],
                            DOB = (DateTime)sqlDataReader["DOB"],
                            Age = (int)sqlDataReader["Age"],
                            Gender = (string)sqlDataReader["Gender"],
                            Qualification = (string)sqlDataReader["Qualification"],
                            Specailization = (string)sqlDataReader["Specialization"],
                            Experience = (int)sqlDataReader["Experience"],
                            DoctorImage = (string)sqlDataReader["DoctorImage"],
                            IsTrash = (bool)sqlDataReader["IsTrash"],
                            CreatedAt = (DateTime)sqlDataReader["CreatedAt"],
                            UpdatedAt = (DateTime)sqlDataReader["UpdatedAt"]

                        };
                        doctors.Add(doctorModel);
                    }
                    return doctors.ToList();
                }
                    return null;


                }catch(Exception e)
            {
                throw e;
               
            }
            finally
            {
                conn.Close();
                
            }
            
        }

    }
}
