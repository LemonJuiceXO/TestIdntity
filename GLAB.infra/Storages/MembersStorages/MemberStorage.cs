    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLAB.Domains.Models.Members;
using Microsoft.Extensions.Configuration;

namespace GLAB.Infra.Storages.MembersStorages
{
    public class MemberStorage : IMemberStorage
    {
        private readonly string connectionString;

        public MemberStorage(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("GlabDB");
        }

        public async Task DeleteMember(string id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            string updateCommandText = @"
        UPDATE dbo.Members 
        SET Status = -1 
        WHERE Id = @MemberId";

            SqlCommand cmd = new SqlCommand(updateCommandText, connection);

            cmd.Parameters.AddWithValue("@MemberId", id);

            await cmd.ExecuteNonQueryAsync();
        }

      

        public async Task InsertMember(Member member)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new("INSERT INTO dbo.Members(MemberId, FirstName, LastName, Email, NIC, PhoneNumber, Logo) " +
                                 "VALUES(@MemberId, @FirstName, @LastName, @Email, @NIC, @PhoneNumber, @Logo)", connection);

            cmd.Parameters.AddWithValue("@MemberId", member.MemberId);
            cmd.Parameters.AddWithValue("@FirstName", member.FirstName);
            cmd.Parameters.AddWithValue("@LastName", member.LastName);
            cmd.Parameters.AddWithValue("@Email", member.Email);
            cmd.Parameters.AddWithValue("@NIC", member.NIC);
            cmd.Parameters.AddWithValue("@PhoneNumber", member.PhoneNumber);
            cmd.Parameters.AddWithValue("@Logo", member.Logo);

            await connection.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<Member>> SelectMembers()
        {
            List<Member> members = new List<Member>();
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new("SELECT * FROM dbo.Members", connection);

            DataTable dt = new();
            SqlDataAdapter da = new(cmd);

            await connection.OpenAsync();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Member member = GetMemberFromDataRow(row);
                members.Add(member);
            }

            return members;
        }

        public async Task UpdateMember(Member member)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string updateCommandText = @"
        UPDATE dbo.Members 
        SET FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email,
            NIC = @NIC,
            PhoneNumber = @PhoneNumber,
            Logo = @Logo
        WHERE MemberId = @MemberId";

            SqlCommand cmd = new SqlCommand(updateCommandText, connection);

            cmd.Parameters.AddWithValue("@FirstName", member.FirstName);
            cmd.Parameters.AddWithValue("@LastName", member.LastName);
            cmd.Parameters.AddWithValue("@Email", member.Email);
            cmd.Parameters.AddWithValue("@NIC", member.NIC);
            cmd.Parameters.AddWithValue("@PhoneNumber", member.PhoneNumber);
            cmd.Parameters.AddWithValue("@Logo", member.Logo);
            cmd.Parameters.AddWithValue("@MemberId", member.MemberId);

            await cmd.ExecuteNonQueryAsync();
        }
        private static Member GetMemberFromDataRow(DataRow row)
        {
            return new Member
            {
                MemberId = (string)row["MemberId"],
                FirstName = (string)row["FirstName"],
                LastName = (string)row["LastName"],
                Email = (string)row["Email"],
                NIC = (string)row["NIC"],
                PhoneNumber = (string)row["PhoneNumber"],
                Logo = (byte[])row["Logo"]
            };
        }



        private readonly string SelectMemberByIdCommand = "select * from members where MemberId=@aMemberId";
        
        public async Task<Member> SelectMemberByUserId(string UserId)
        {
            using (var connection = new SqlConnection(connectionString)
                   
                   )
            {
                SqlCommand command = new SqlCommand(SelectMemberByIdCommand,connection);

                await connection.OpenAsync();
                
                DataTable dt = new ();
                SqlDataAdapter da = new(command);

                command.Parameters.AddWithValue("@aMemberID", UserId);
                da.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    var member=  GetMemberFromDataRow(dt.Rows[0]);
                    return member;
                }    
                
                  
               

            }

            return null;
        }
        
        
    }
    
}
    
