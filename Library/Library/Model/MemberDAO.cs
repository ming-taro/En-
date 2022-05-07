using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class MemberDAO
    {
        private static MemberDAO memberDAO;
        private string connectionString;
        private MySqlConnection connection;
        private MySqlCommand command;
        private MemberDAO()
        {
            connectionString = Constants.SERVER + Constants.PORT + Constants.DATABASE + Constants.ID + Constants.PASSWORD;
        }
        public static MemberDAO GetInstance()
        {
            if(memberDAO == null)
            {
                memberDAO = new MemberDAO();
            }
            return memberDAO;
        }
        private void OpenConnection()
        {
            if(connection == null || connection.State != ConnectionState.Open)
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();

            }
        }
        public AdminDTO GetAdminAccount()   //관리자 계정
        {
            AdminDTO admin;

            OpenConnection();
            command = new MySqlCommand(Constants.ADMIN_ACCOUNT, connection);
            MySqlDataReader table = command.ExecuteReader();

            table.Read();
            admin = new AdminDTO(table["id"].ToString(), table["password"].ToString());
            table.Close();
            connection.Close();

            return admin;
        }
        public MemberVO GetMemberAccount(string memberId)   //입력받은 id와 일치하는 회원계정
        {
            MemberVO member;

            OpenConnection();
            command = new MySqlCommand(Constants.MEMBER_ACCOUNT, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));

            MySqlDataReader table = command.ExecuteReader();
            table.Read();
            member = new MemberVO(table["id"].ToString(), table["password"].ToString(), table["name"].ToString(), table["age"].ToString(), table["phoneNumber"].ToString(), table["address"].ToString());
            connection.Close();

            return member;
        }
        public List<MemberVO> MakeMemberList()  //회원목록
        {
            List<MemberVO> memberList = new List<MemberVO>();

            OpenConnection();
            command = new MySqlCommand(Constants.MEMBER_LIST, connection);
            MySqlDataReader table = command.ExecuteReader();

            while (table.Read())
            {
                memberList.Add(new MemberVO(table["id"].ToString(), table["password"].ToString(), table["name"].ToString(), table["age"].ToString(), table["phoneNumber"].ToString(), table["address"].ToString()));
            }
            table.Close();
            connection.Close();

            return memberList;
        }
        public bool IsExistingMember(string memberId, string password)  //입력된 정보가 존재하는 회원인지 확인
        {
            OpenConnection();
            command = new MySqlCommand(Constants.MEMBER_CONFIRMATION, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));
            command.Parameters.Add(new MySqlParameter("@password", password));

            MySqlDataReader table = command.ExecuteReader();
            table.Read();
            if (table.HasRows)
            {
                table.Close();
                connection.Close();
                return Constants.IS_EXISTING_MEMBER;
            }
            else
            {
                table.Close();
                connection.Close();
                return Constants.IS_NON_EXISTING_MEMBER;
            }
        }
        public bool IsDuplicateMemberId(string memberId)  //기존 회원의 아이디와 중복되는지 확인
        {
            OpenConnection();
            command = new MySqlCommand(Constants.DUPLICATE_MEMBER_ID, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));

            MySqlDataReader table = command.ExecuteReader();
            table.Read();
            if (table.HasRows)
            {
                table.Close();
                connection.Close();
                return Constants.IS_DUPLICATE_ID;
            }
            else
            {
                table.Close();
                connection.Close();
                return Constants.IS_NON_DUPLICATE_ID;
            }
        }
        public void AddToMemberList(string query, MemberVO member)   //회원목록에 회원정보 추가
        {
            OpenConnection();
            command = new MySqlCommand(query, connection);
            command.Parameters.Add(new MySqlParameter("@id", member.Id));
            command.Parameters.Add(new MySqlParameter("@password", member.Password));
            command.Parameters.Add(new MySqlParameter("@name", member.Name));
            command.Parameters.Add(new MySqlParameter("@age", member.Age));
            command.Parameters.Add(new MySqlParameter("@phoneNumber", member.PhoneNumber));
            command.Parameters.Add(new MySqlParameter("@address", member.Address));
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteFromMemberList(string memberId)      //회원목록에서 회원정보 삭제
        {
            OpenConnection();
            command = new MySqlCommand(Constants.DELETION_FROM_MEMBER_LIST, connection);
            command.Parameters.Add(new MySqlParameter("@memberId", memberId));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
