namespace Horeca.Permissions;

public static class HorecaPermissions
{
    public const string Horeca = "Horeca";
    public const string Create = ".Create";
    public const string Edit = ".Edit";
    public const string Delete = ".Delete";
    public const string Read = ".Read";
    public const string Supplier = ".Supplier";
    public const string Customer = ".Customer";
    public const string Admin = ".Admin";

    public const string ProductManagement = "ProductManagement";

    public const string ProductManagementSupplier = ProductManagement + Supplier;
    public const string ProductManagementAdmin = ProductManagement + Admin;

    public const string AddressManagement = "AddressManagement";
    public const string AddressManagementSupplier = AddressManagement + Supplier;
    public const string AddressManagementCustomer = AddressManagement + Customer;

    public const string Order = "Order";

    public const string OrderCreate = Order + Create;
    public const string OrderEdit = Order + Edit;
    public const string OrderDelete = Order + Delete;
    public const string OrderRead = Order + Read;

    public const string OrderManagement = "OrderManagement";
    public const string OrderManagementSupplier = OrderManagement + Supplier;
    public const string OrderManagementCustomer = OrderManagement + Customer;

    public const string Category = "Category";

    public const string CategoryCreate = Category + Create;
    public const string CategoryEdit = Category + Edit;
    public const string CategoryDelete = Category + Delete;
    public const string CategoryRead = Category + Read;
}
