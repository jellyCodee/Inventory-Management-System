# Inventory Management System Mid Semester Project - Programming 2


A windows application inventory management system built in C#. The aplication comes with features typical of a sales inventory system running on an SQL database.

Link to Youtube video: https://youtu.be/lXAy1d6eKb8

Below are the features the application comes with;

1. `Welcome Screen`: The welcome screen welcomes a user when they successfully launch the application. There are two buttons that allow for `Admin` login or `Attendant` login.
The dashboard will allow some buttons and features based on if the user is an admin or an attendant.
![](/images/1.png)


2. `Login Screen`: The login screen allows for authentication using `username` and `password`. The application checks for validity of the user input before allowing or denying access. 
There is a button to switch between showing password and not.
![](/images/2.png)


### Dashboard


The dashboard has a sidebar for navigation and a main panel to display table content.


Here are the navigation features;

1. `Products`: The products button brings up the products table which displays the products in stock. Products comprise of columns like: `product_id`, `name`, `quantity`,
`price`, `description` and `category`. A product can be edited or deleted from the stock. There is a `new` button to add new products to the stock.

2. `Categories`: Just like the `products` section the categories section also has an add button to add a new category to the database. category columns include: `category_id`
and `name`.

3. `Customers`: This section contains the customers in the system. Customers can be added, edited or deleted. The columns for this section are: `customer_id`, `name` and `phone number`.

4. `Users`: This section comprises of the users in the system, mainly attendants and the admin. Only the admin has access to this section as attendants deal with customers only.

5. `Sales`: The sales section comprises of daily and paste sales. Every purchase made by a customer is stores here with the customer details and product details. 
When a sales is made, a sales invoice/reciept is generated which can then be passed on to the customer. A sales can also be edited for cases the abide by the company's return policies.
![](/images/3.png)
![](/images/4.png)

6. `Log out`: The log out button logs the user out the system.
