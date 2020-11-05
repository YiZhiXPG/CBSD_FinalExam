using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExam.Models
{
    public class ProblemDAL
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PROBLEMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Get All
        public IEnumerable<ProblemInfo> GetAllProblem()
        {
            List<ProblemInfo> proList = new List<ProblemInfo>();

            using(SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetAllProblem", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ProblemInfo pro = new ProblemInfo();
                    pro.ID = Convert.ToInt32(dr["ID"].ToString());
                    pro.Name = dr["Name"].ToString();
                    pro.Detial = dr["Detial"].ToString();

                    proList.Add(pro);
                }
                con.Close();
            }
            return proList;
        }

        //To Insert Problem
        public void AddProblem(ProblemInfo pro)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertProblem", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", pro.Name);
                cmd.Parameters.AddWithValue("@Detail", pro.Detial);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        //To Update Problem
        public void UpdateProblem(ProblemInfo pro)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_UpdateProblem", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", pro.Name);
                cmd.Parameters.AddWithValue("@Detial", pro.Detial);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        internal ProblemInfo GetProblemById(int? id)
        {
            throw new NotImplementedException();
        }

        //To Delete Problem
        public void DeleteProblem(int? proId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_DeleteProblem", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProId", proId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        // Get problem by ID
        public ProblemInfo GetProblemById(int proId)
        {
            ProblemInfo pro = new ProblemInfo();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_GetProblemById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProId", proId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    
                    pro.ID = Convert.ToInt32(dr["ID"].ToString());
                    pro.Name = dr["Name"].ToString();
                    pro.Detial = dr["Detial"].ToString();

                    
                }
                con.Close();
            }
            return pro;
        }
    }
}
