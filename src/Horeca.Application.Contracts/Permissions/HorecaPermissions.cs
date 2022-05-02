namespace Horeca.Permissions;

public static class HorecaPermissions
{
    public const string Horeca = "Horeca";
    public const string Create = ".Create";
    public const string Edit = ".Edit";
    public const string Delete = ".Delete";
    public const string Read = ".Read";

    public const string ProductBid = "ProductBid";

    public const string ProductBidCreate = ProductBid + Create;
    public const string ProductBidEdit = ProductBid + Edit;
    public const string ProductBidDelete = ProductBid + Delete;
    public const string ProductBidRead = ProductBid + Read;

    public const string ProductManagement = "ProductManagement";

    public const string ProductCreate = ProductManagement + Create;
    public const string ProductEdit = ProductManagement + Edit;
    public const string ProductDelete = ProductManagement + Delete;
    public const string ProductRead = ProductManagement + Read;
    public const string ProductApprove = ProductManagement + "Approve";

    public const string Category = "Category";

    public const string CategoryCreate = Category + Create;
    public const string CategoryEdit = Category + Edit;
    public const string CategoryDelete = Category + Delete;
    public const string CategoryRead = Category + Read;
}
