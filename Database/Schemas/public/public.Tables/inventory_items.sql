-- Table: public.inventory_items

-- DROP TABLE public.inventory_items;

CREATE TABLE public.inventory_items
(
    code text COLLATE pg_catalog."default" NOT NULL DEFAULT nextval('master_seq'::regclass),
    description text COLLATE pg_catalog."default" NOT NULL,
    category text COLLATE pg_catalog."default",
    comment text COLLATE pg_catalog."default",
    sellprice double precision,
    sellcurrency text COLLATE pg_catalog."default",
    buyprice double precision,
    buycurrency text COLLATE pg_catalog."default",
    stockvalue double precision,
    supplier_fk integer,
    salesacct integer,
    stockacct integer,
    cogacct integer,
    sohcount double precision,
    stocktakenewqty double precision,
    flags integer,
    qtybrksellprice double precision,
    costprice double precision,
    isconversion boolean,
    conversionsource text COLLATE pg_catalog."default",
    useraddedby text COLLATE pg_catalog."default",
    userupdatedby text COLLATE pg_catalog."default",
    dateadded timestamp without time zone,
    dateupdated timestamp without time zone,
    sellunit_fk integer,
    buyunit_fk integer,
    CONSTRAINT inventory_items_pkey PRIMARY KEY (code),
    CONSTRAINT inventory_items_buycurrency_fk FOREIGN KEY (buycurrency)
        REFERENCES admin.currency_types (abbreviation) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT inventory_items_buyunit_fk FOREIGN KEY (buyunit_fk)
        REFERENCES admin.units (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT inventory_items_category_fk FOREIGN KEY (category)
        REFERENCES admin.inventory_categories (name) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT inventory_items_sellcurrency_fk FOREIGN KEY (sellcurrency)
        REFERENCES admin.currency_types (abbreviation) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT inventory_items_sellunit_fk FOREIGN KEY (sellunit_fk)
        REFERENCES admin.units (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.inventory_items
    OWNER to postgres;

-- Trigger: insert_inventory_auditcolumns

-- DROP TRIGGER insert_inventory_auditcolumns ON public.inventory_items;

CREATE TRIGGER insert_inventory_auditcolumns
    BEFORE INSERT
    ON public.inventory_items
    FOR EACH ROW
    EXECUTE PROCEDURE public.initialize_audit_columns();

-- Trigger: update_inventory_auditcolumns

-- DROP TRIGGER update_inventory_auditcolumns ON public.inventory_items;

CREATE TRIGGER update_inventory_auditcolumns
    BEFORE UPDATE 
    ON public.inventory_items
    FOR EACH ROW
    EXECUTE PROCEDURE public.update_audit_columns();