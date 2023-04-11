using System;
using System.Web.UI;
using Firebase.Auth;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace FoodDelivery
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            label_error_email.Visible = false;
            label_error_password.Visible = false;
            label_error_signin.Visible = false;
        }

        private bool Check_Input_Data()
        {
            bool validEmail = Is_Valid_Email(text_email.Text);

            bool validPassword = text_password.Text != String.Empty ? true : false;

            if(validEmail == false)
            {
                label_error_email.Visible = true;
            }
            else 
            {
                label_error_email.Visible = false;
            }

            if (validPassword == false)
            {
                label_error_password.Visible = true;
            }
            else
            {
                label_error_password.Visible = false;
            }

            return validEmail && validPassword;
        }
        
        static async Task<FirebaseAuthLink> Try_To_Connect(String email, String password)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBF7qaCQxjHkDZC2fKKxKFAMeJB5w2HYP8"));

            try
            {
                var authResult = await auth.SignInWithEmailAndPasswordAsync(email, password);

                return authResult;
            }
            catch
            {
                return null;
            }
        }

        static async Task<String> Wait_To_Sign_in(TextBox email, TextBox password)
        {
            var task = await Try_To_Connect(email.Text, password.Text);

            if(task != null)
            {
                return task.User.LocalId;
            }

            return String.Empty;
        }

        protected void Sing_In_Button_Pressed(object sender, EventArgs e)
        {
            if (Check_Input_Data())
            {
                Task<String> task = Task<String>.Run(async () => await Wait_To_Sign_in(text_email, text_password));

                if(task.Result == String.Empty)
                {
                    label_error_signin.Visible = true;
                }
                else
                {
                    Application["user_id"] = task.Result;

                    string url = "../Connected/Connected.aspx";
                    Response.Redirect(url);
                }
            }
        }

        bool Is_Valid_Email(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}