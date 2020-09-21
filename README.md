# LifeServeBD (An Online Medical Store)

LifeServeBD is an e-commerce website.

# Features:

  - All types of medicines are available
  - Detail view of individual medicine
  - Medicines are categorized as regular and non regular
  - Medicines can be found according to the company
  - searching medicines according to their names
  - sorting medicines according to price and names in ascending and descending order
  - choosing the product by quantity for view
  - searching alternate medicines (search by component)
  - view alternatives (cheaper and less sideeffectful suggestions will be provided)
  - Reknowned Doctors' Informations
  - Selecting Doctors' info according to category
  - Detail view of the selected doctor
  - Login/Registration
  - Account (editing account)
  - Add to Cart
  - Place Order
  - Checking Prescription if non-regular medicine is present in the cart
  - Checking Customer validation input
  - Checking if the cart has minimum one item for order
  - Return message whether order is accepted or cancelled
  - Helpline 
  - Terms and Conditions
  - Privacy Policy
  - About Us
  - Admin Panel for managing and handling all these

### Tech
Our Website is built on MVC pattern

* Asp.net - Framework
* C# - Backend Language
* HTML - Frontend Language
* CSS - Frontend Language
* Bootstrap - Frontend Framework
* Javascript - Frontend Language


### Installation

LifeServeBD requires:

  - Installation of SQL Server Management Studio 2019, SQL Server Installation:
https://www.youtube.com/watch?v=MnLZuK2DME8&t=1s
  - Visual Studio 2019 
### Process to follow after the installation

  - First of all, the mdf file which was given is needed to be attached with the SQL server.
  - Then a database connection Of SQL Server is needed to be built with Visual Studio
  - Here is a link given how to connect sql server with visual studio. link - https://www.youtube.com/watch?v=jOjzoVTmLqo 
  - After connected succesfully, user needs go to the Server Explorer option in visual studio. Here all the databeses are found that were created in sql server studio. User needs to choose the database that were attached before and click on it. Then he will find some properties in the solution explorer.There the user can find a connection string from the Connection block. For example, in my computer, the connection string is "Data Source=DESKTOP-CIV7264\SQLEXPRESS;Initial Catalog=LifeServeBD_DB;Integrated Security=True"
  - The connection string needs to be copied and paste in all the Controllers. Already there are connection strings present in all the controllers. User needs to replace it with his own one.
  -In The controllers where the connectection string need to be pasted will be found after "public class ......Controller : Controller"
            - AboutUsController
            - AccountController
            - AdminController
            - AdminOrderController
            - CartController
            - CustomerController
            - DoctorController
            - HelplineController
            - HomeController
            - OrderController
            - PaymentCategory
            - PolicyTermsController
            - ProductController
            - UserController
            - UserNotificationController
            
- Now user needs to click on the AppData folder and open the mdf file before running. Then he needs to check if the database in the server explorer becomes green. If it becomes green then it is attached successfully.
Now the project is ready to run. :D

### Website Overview
- In the homepage user can see some of our products with an interactive UI
- In the Navbar there are options for medicines, doctors's info, 
- Login is a must for adding product in the cart and order those products
- If anyone does not have any account in the website then first he needs to sign up by providing to the required info
- After clicking signup a link will be send to his mail account. He can login through this link for one time. After this he needs to go to login page and provide his mail id and password for login
- Forgot Password option is provided also. If a user forgets his password then he can set his password again through his mail id
- In medicines page user can search and view all the features given above
- In doctors Info user can view all the features given above
- In account page user can upload his image, edit his name , address, password etc.
- In helpline user can ask for any valid help by providing the required info.
- If any user's order is accepted or cancelled due to some issues then a message will be send. The user can see those messages by clicking the message icon. But to view this he needs to login first.
- Terms and Conditions, Privacy Policy and About Us pages are attached
- In admin Panel Admin can be able to manage all the things
- Admin needs to login first to enter in the admin panel
- Admin can insert new products, edit products, delete products search products
- Admin can insert new doctor info, edit , delete and search
- In order table admin will find all the orders , he can delete any order if necessary
- In Customer Table admin can find who ordered products, their address, phone etc. If a prescription is provided with any order admin can find it in this table.There are two options for admin here. Accept and Reject. If a order is accepted then Admin will click accept button and an acceptation message will be sent to user's messagebox. If the order is rejected for prescription credentials or any issues, then Admin will click reject button and a rejection message will be sent to user's message box
- In payment table admin will find the total amount for each order along with the delivery charge. If a payment is received then admin will click received button and the current date and time will be saved for receiving payment.
- In user table admin will find all the registered users' info.
- In helpline table all the questions asked by the customers will be found with their personal informations
- In privacy policy and terms and conditions tables, admin can edit if any changes are necessary.
- admin can add other admins if necessary.











