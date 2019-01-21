# OnlineStoreBackend

This file contains the documentation for the Online Store Backend API. It describes which API end points to call to perform core activities like product lookup, adding a product to a cart, and checking out (completing the cart).

To get a product with a certain productId:

https://{ApplicationDomainName}/api/Product/GetProduct?productId={productId}

To get all products:
    
https://{ApplicationDomainName}/api/Product/GetAllProducts 

https://{ApplicationDomainName}/api/Product/GetAllProducts?getProductsWithAvailableInventory=true (To only get products with available inventory)

To purchase a product:

https://{ApplicationDomainName}/api/Product/Purchase?productId={productId} 

To see all the items in the cart:

https://{ApplicationDomainName}/api/Cart/Info

To complete a cart:

https://{ApplicationDomainName}/api/Cart/Complete
