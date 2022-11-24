USE DanielD26

IF OBJECT_ID('CATEGORY') IS NOT NULL
DROP TABLE CATEGORY;

CREATE TABLE CATEGORY (
    CatID   INT,
    CatName VARCHAR(100),
    PRIMARY KEY (CatID)
);

IF OBJECT_ID('SEGMENT') IS NOT NULL
DROP TABLE SEGMENT;

CREATE TABLE SEGMENT (
    SegID   INT,
    SegName VARCHAR(100),
    PRIMARY KEY (SegID)
);

IF OBJECT_ID('PRODUCT') IS NOT NULL
DROP TABLE PRODUCT;

CREATE TABLE PRODUCT (
    ProductID       VARCHAR(100),
    CatID           INT,
    [Description]   VARCHAR (100),
    UnitPrice       INT,
    FOREIGN KEY (CatID) REFERENCES CATEGORY,
    PRIMARY KEY (ProductID)
);

IF OBJECT_ID('REGION') IS NOT NULL
DROP TABLE REGION;

CREATE TABLE REGION (
    Region VARCHAR(100),
    PRIMARY KEY (Region)
);

IF OBJECT_ID('CUSTOMER') IS NOT NULL
DROP TABLE CUSTOMER;

CREATE TABLE CUSTOMER (
    CustID      VARCHAR(100),
    FullName    VARCHAR(100),
    SegID       INT,
    Country     VARCHAR(100),
    City        VARCHAR(100),
    [State]     VARCHAR(100),
    PostCode    INT,
    Region      VARCHAR(100),
    FOREIGN KEY (SegID) REFERENCES SEGMENT,
    FOREIGN KEY (Region) REFERENCES REGION,
    PRIMARY KEY (CustID)
);

IF OBJECT_ID('SHIPPING') IS NOT NULL
DROP TABLE SHIPPING;

CREATE TABLE SHIPPING (
    ShipMode VARCHAR(100),
    PRIMARY KEY (ShipMode)
);

IF OBJECT_ID('ORDER') IS NOT NULL
DROP TABLE [ORDER];

CREATE TABLE [ORDER] (
    OrderDate   NVARCHAR (100),
    CustID      VARCHAR(100),
    ProductID   VARCHAR(100),
    Quantity    INT,
    ShipDate    NVARCHAR (100),
    ShipMode    VARCHAR(100),
    FOREIGN KEY (CustID) REFERENCES CUSTOMER,    
    FOREIGN KEY (ProductID) REFERENCES PRODUCT,    
    FOREIGN KEY (ShipMode) REFERENCES SHIPPING,    
    PRIMARY KEY (OrderDate, CustID, ProductID)
);

INSERT INTO CATEGORY (CatID, CatName) VALUES
    (1, 'Furniture'),
    (2, 'Office Supplies'),
    (3, 'Technology');

INSERT INTO SEGMENT (SegID, SegName) VALUES
    (1, 'Consumer'),
    (2, 'Corporate'),
    (3, 'Home Office');

INSERT INTO PRODUCT (ProductID, CatID, [Description], UnitPrice) VALUES
    ('FUR-BO-10001798', 1, 'Bush Somerset Collection Bookcase', 261.96),
    ('FUR-CH-10000454', 3, 'Mitel 5320 IP Phone VoIP phone', 731.94),
    ('OFF-LA-10000240', 2, 'Self-Adhesive Address Labels for Typewriters by Universal', 14.62);

INSERT INTO REGION (Region) VALUES
    ('South'),
    ('Central'),
    ('West'),
    ('East'),
    ('North');

INSERT INTO CUSTOMER (CustID, FullName, SegID, Country, City, [State], PostCode, Region) VALUES
    ('CG-12520', 'Claire Gute', 1, 'United States', 'Henderson', 'Oklahoma', 42420, 'Central'),
    ('DV-13045', 'Darrin Van Huff', 2, 'United States', 'Los Angeles', 'California', 90036, 'West'),
    ('SO-20335', 'Sean O"Donnell', 1, 'United States', 'Fort Lauderdale', 'Florida', 33311, 'South'),
    ('BH-11710', 'Brosina Hoffman', 3, 'United States', 'Los Angeles', 'California', 90032, 'West');
    
INSERT INTO SHIPPING (ShipMode) VALUES
    ('Second Class'),
    ('Standard Class'),
    ('First Class'),
    ('Overnight Express');

INSERT INTO [ORDER] (CustID, ProductID, OrderDate, Quantity, ShipDate, ShipMode) VALUES
    ('CG-12520', 'FUR-BO-10001798', '2016/11/08', 2, '2016/11/11', 'Second Class'),
    ('CG-12520', 'FUR-CH-10000454', '2016/11/08', 3, '2016/11/11', 'Second Class'),
    ('CG-12520', 'OFF-LA-10000240', '2016/06/12', 2, '2016/06/16', 'Second Class'),
    ('DV-13045', 'OFF-LA-10000240', '2015/11/21', 2, '2015/11/16', 'Second Class'),
    ('DV-13045', 'OFF-LA-10000240', '2014/10/11', 1, '2014/10/15', 'Standard Class'),
    ('DV-13045', 'FUR-CH-10000454', '2016/11/12', 9, '2016/11/16', 'Standard Class'),
    ('SO-20335', 'OFF-LA-10000240', '2016/09/02', 5, '2016/09/08', 'Standard Class'),
    ('SO-20335', 'FUR-BO-10001798', '2017/08/25', 2, '2017/08/29', 'Overnight Express'),
    ('SO-20335', 'FUR-CH-10000454', '2017/06/22', 2, '2017/06/26', 'Standard Class'),
    ('SO-20335', 'FUR-BO-10001798', '2017/05/01', 3, '2017/05/02', 'First Class');

------------------------------------------- POST ORDER --------------------------------------------------------
GO
IF OBJECT_ID('PostOrder') IS NOT NULL
DROP PROCEDURE PostOrder
GO
CREATE PROCEDURE PostOrder
    @pCustID NVARCHAR (100),
    @pProductID NVARCHAR (100),
    @pOrderDate NVARCHAR (100),
    @pQuantity INT,
    @pShipDate NVARCHAR(100),
    @pShipMode NVARCHAR(100)
AS
    INSERT INTO [ORDER] (OrderDate, CustID, ProductID, Quantity, ShipDate, ShipMode)
    VALUES (@pOrderDate, @pCustID, @pProductID, @pQuantity, @pShipDate, @pShipMode)
GO

-- EXEC PostOrder @pOrderDate = '9999/99/99', @pCustID = "CG-12520", @pProductID = "FUR-BO-10001798", @pQuantity = 10, @pShipDate = '2022/05/01', @pShipMode = "First Class";
------------------------------------------- PUT ORDER --------------------------------------------------------
IF OBJECT_ID('PutOrder') IS NOT NULL
DROP PROCEDURE PutOrder
GO

CREATE PROCEDURE PutOrder 
    @pOrderDate NVARCHAR (100),    
    @pCustID NVARCHAR (100),
    @pProductID NVARCHAR (100),
    @pQuantity INT,
    @pShipDate NVARCHAR (100),
    @pShipMode NVARCHAR(100)
AS
    UPDATE [ORDER]
    SET ProductID = @pProductID, Quantity = @pQuantity, ShipDate = @pShipDate, ShipMode = @pShipMode
    WHERE OrderDate = @pOrderDate AND CustID = @pCustID;

GO

-- EXEC PutOrder @pOrderDate = '9999/99/99', @pCustID = "CG-12520", @pProductID = "FUR-BO-10001798", @pQuantity = 5555, @pShipDate = '2022/05/01', @pShipMode = "First Class";
------------------------------------------- DELETE ORDER --------------------------------------------------------
IF OBJECT_ID('DeleteOrder') IS NOT NULL
DROP PROCEDURE DeleteOrder
GO

CREATE PROCEDURE DeleteOrder
    @pOrderDate NVARCHAR(100),
    @pCustID NVARCHAR (100),
    @pProductID NVARCHAR (100)
AS
    DELETE FROM [ORDER]
    WHERE OrderDate = @pOrderDate AND CustID = @pCustID AND ProductID = @pProductID;


/*
SELECT * FROM CATEGORY;
SELECT * FROM SEGMENT;
SELECT * FROM PRODUCT;
SELECT * FROM REGION;
SELECT * FROM CUSTOMER;
SELECT * FROM SHIPPING;
SELECT * FROM [ORDER];

DROP TABLE [ORDER];
DROP TABLE [CUSTOMER];
DROP TABLE [PRODUCT];
DROP TABLE [REGION];
DROP TABLE [SHIPPING];
DROP TABLE [SEGMENT];
DROP TABLE [CATEGORY];

*/
