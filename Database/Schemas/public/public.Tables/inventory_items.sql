-- Table: public.inventory_items

-- DROP TABLE public.inventory_items;

CREATE TABLE public.inventory_items
(
    code text COLLATE pg_catalog."default" NOT NULL DEFAULT nextval('inventory_items_code_seq'::regclass),
    description text COLLATE pg_catalog."default" NOT NULL,
    stockvalue double precision,
    sellprice double precision,
    comment text COLLATE pg_catalog."default",
    supplier_fk integer,
    salesacct integer,
    stockacct integer,
    cogacct integer,
    stocktakenewqty double precision,
    flags integer,
    sohcount double precision,
    buyprice double precision,
    buyunit text COLLATE pg_catalog."default",
    qtybrksellprice double precision,
    sellcurrency text COLLATE pg_catalog."default",
    buycurrency text COLLATE pg_catalog."default",
    costprice double precision,
    sellunit text COLLATE pg_catalog."default",
    category text COLLATE pg_catalog."default",
    isconversion boolean,
    conversionsource text COLLATE pg_catalog."default",
    useraddedby text COLLATE pg_catalog."default",
    userlastupdatedby text COLLATE pg_catalog."default",
    dateadded timestamp without time zone,
    dateupdated timestamp without time zone,
    CONSTRAINT inventory_items_pkey PRIMARY KEY (code)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.inventory_items
    OWNER to postgres;

GRANT ALL ON TABLE public.inventory_items TO administrator;

GRANT ALL ON TABLE public.inventory_items TO postgres;

GRANT DELETE, UPDATE, SELECT, INSERT ON TABLE public.inventory_items TO sourcing_team_member;

-- Trigger: insert_inventory_auditcolumns

-- DROP TRIGGER insert_inventory_auditcolumns ON public.inventory_items;

CREATE TRIGGER insert_inventory_auditcolumns
    BEFORE INSERT
    ON public.inventory_items
    FOR EACH ROW
    EXECUTE PROCEDURE initialize_audit_columns();

-- Trigger: update_inventory_auditcolumns

-- DROP TRIGGER update_inventory_auditcolumns ON public.inventory_items;

CREATE TRIGGER update_inventory_auditcolumns
    BEFORE UPDATE 
    ON public.inventory_items
    FOR EACH ROW
    EXECUTE PROCEDURE update_audit_columns();