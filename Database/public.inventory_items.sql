-- Table: public.inventory_items

-- DROP TABLE public.inventory_items;

CREATE TABLE public.inventory_items
(
    code text COLLATE pg_catalog."default" NOT NULL DEFAULT nextval('inventory_items_code_seq'::regclass),
    description text COLLATE pg_catalog."default" NOT NULL,
    stockonhand double precision,
    stockvalue double precision,
    sellingprice double precision,
    comment text COLLATE pg_catalog."default",
    supplier_fk integer,
    salesacct integer,
    stockacct integer,
    cogacct integer,
    sellingunit integer,
    addedby integer,
    lastupdatedby integer,
    stocktakenewqty integer,
    flags integer,
    minbuildqty integer,
    normalbuildqty integer,
    sohcount integer,
    costprice double precision,
    buyingunitid integer,
    buyingunit text COLLATE pg_catalog."default",
    migr_counted text COLLATE pg_catalog."default",
    buyingprice double precision,
    category integer,
    dateadded date,
    CONSTRAINT inventory_items_pkey PRIMARY KEY (code),
    CONSTRAINT inventory_items_category_fkey FOREIGN KEY (category)
        REFERENCES admin.inventory_categories (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT inventory_items_sellingunit_id_fkey FOREIGN KEY (sellingunit)
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

GRANT ALL ON TABLE public.inventory_items TO administrator;

GRANT ALL ON TABLE public.inventory_items TO postgres;

GRANT DELETE, UPDATE, SELECT, INSERT ON TABLE public.inventory_items TO sourcing_team_member;