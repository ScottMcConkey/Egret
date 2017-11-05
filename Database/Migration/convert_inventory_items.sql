INSERT INTO public.inventory_items(
	code, 
	description, 
	stockvalue, 
	sellprice, 
	comment, 
	salesacct, 
	stockacct, 
	cogacct, 
	sellunit, 
	stocktakenewqty, 
	flags,  
	sohcount, 
	buyprice, 
	buyunit, 
	buyprice, 
	category, 
	qtybrksellprice, 
	sellcurrency, 
	buycurrency, 
	costprice
	)
	VALUES (
	"Code", --code 
	"Description", --description, 
	select cast("StockValue" as double precision), --stockvalue
	select cast("SellPrice" as double precision), --sellprice
	'CONVERSION: \n' || 
		'Original Comment: ' || "Comment" || '\n' ||
		'Original SellUnit: ' || "SellUnit" || '\n' ||
		'Original BuyUnit: ' || "BuyUnit" || '\n' ||
		'Original Buy Currency: ' || "BuyPriceCurrency" || '\n', --comment
	select cast("SalesAcct" as integer), --salesacct
	select cast("StockAcct" as integer), --stockacct
	select cast("COGAcct" as integer), --cogacct
	"SellUnit", --sellunit
	select cast("StockTakeNewQty" as integer), --stocktakenewqty
	select cast("Flags" as integer), --flags
	select cast("SOH/Count" as integer), --sohcount
	select cast("BuyPrice" as double precision), --buyprice
	"BuyUnit", --buyunit
	select cast("BuyPrice" as double precision), --buyprice
	"Category1", --category
	select cast("QtyBrkSellPriceA1" as double precision), --qtybrksellprice
	"BuyPriceCurrency", --sellcurrency
	"BuyPriceCurrency", --buycurrency
	select cast("CostPrice" as double precision), --costprice
	);