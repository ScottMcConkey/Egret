-- Table: public.migrate_inventory

-- DROP TABLE public.migrate_inventory;

CREATE TABLE public.migrate_inventory
(
    "Code" text COLLATE pg_catalog."default",
    "Supplier" text COLLATE pg_catalog."default",
    "SuppliersCode" text COLLATE pg_catalog."default",
    "Description" text COLLATE pg_catalog."default",
    "Comment" text COLLATE pg_catalog."default",
    "Category1" text COLLATE pg_catalog."default",
    "Category2" text COLLATE pg_catalog."default",
    "Category3" text COLLATE pg_catalog."default",
    "Category4" text COLLATE pg_catalog."default",
    "SalesAcct" text COLLATE pg_catalog."default",
    "StockAcct" text COLLATE pg_catalog."default",
    "COGAcct" text COLLATE pg_catalog."default",
    "SellUnit" text COLLATE pg_catalog."default",
    "SellPrice" text COLLATE pg_catalog."default",
    "SellPriceB" text COLLATE pg_catalog."default",
    "SellPriceC" text COLLATE pg_catalog."default",
    "SellPriceD" text COLLATE pg_catalog."default",
    "SellPriceE" text COLLATE pg_catalog."default",
    "SellPriceF" text COLLATE pg_catalog."default",
    "QtyBrkSellPriceA1" text COLLATE pg_catalog."default",
    "QtyBrkSellPriceA2" text COLLATE pg_catalog."default",
    "QtyBrkSellPriceA3" text COLLATE pg_catalog."default",
    "QtyBrkSellPriceA4" text COLLATE pg_catalog."default",
    "QtyBrkSellPriceB1" text COLLATE pg_catalog."default",
    "QtyBrkSellPriceB2" text COLLATE pg_catalog."default",
    "QtyBrkSellPriceB3" text COLLATE pg_catalog."default",
    "QtyBrkSellPriceB4" text COLLATE pg_catalog."default",
    "QtyBreak1" text COLLATE pg_catalog."default",
    "QtyBreak2" text COLLATE pg_catalog."default",
    "QtyBreak3" text COLLATE pg_catalog."default",
    "QtyBreak4" text COLLATE pg_catalog."default",
    "BuyUnit" text COLLATE pg_catalog."default",
    "BuyPrice" text COLLATE pg_catalog."default",
    "ConversionFactor" text COLLATE pg_catalog."default",
    "SellDiscount" text COLLATE pg_catalog."default",
    "SellDiscountMode" text COLLATE pg_catalog."default",
    "ReorderLevel" text COLLATE pg_catalog."default",
    "Type" text COLLATE pg_catalog."default",
    "Colour" text COLLATE pg_catalog."default",
    "UserNum" text COLLATE pg_catalog."default",
    "UserText" text COLLATE pg_catalog."default",
    "Plussage" text COLLATE pg_catalog."default",
    "BuyWeight" text COLLATE pg_catalog."default",
    "StockTakeNewQty" text COLLATE pg_catalog."default",
    "BarCode" text COLLATE pg_catalog."default",
    "BuyPriceCurrency" text COLLATE pg_catalog."default",
    "Custom1" text COLLATE pg_catalog."default",
    "Custom2" text COLLATE pg_catalog."default",
    "Custom3" text COLLATE pg_catalog."default",
    "Custom4" text COLLATE pg_catalog."default",
    "LeadTimeDays" text COLLATE pg_catalog."default",
    "SellWeight" text COLLATE pg_catalog."default",
    "Flags" text COLLATE pg_catalog."default",
    "MinBuildQty" text COLLATE pg_catalog."default",
    "NormalBuildQty" text COLLATE pg_catalog."default",
    "SOH/Count" text COLLATE pg_catalog."default",
    "StockValue" text COLLATE pg_catalog."default",
    "MarginWarning" text COLLATE pg_catalog."default",
    "CostPrice" text COLLATE pg_catalog."default",
    "StockTakeStartQty" text COLLATE pg_catalog."default",
    "StockTakeValue" text COLLATE pg_catalog."default"
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.migrate_inventory
    OWNER to postgres;