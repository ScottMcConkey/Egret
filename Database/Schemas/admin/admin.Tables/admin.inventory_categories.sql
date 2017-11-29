-- Table: admin.inventory_categories

-- DROP TABLE admin.inventory_categories;

CREATE TABLE admin.inventory_categories
(
    id integer NOT NULL DEFAULT nextval('admin.inventory_categories_id_seq'::regclass),
    name text COLLATE pg_catalog."default" NOT NULL,
    description text COLLATE pg_catalog."default",
    sortorder integer,
    active boolean,
    CONSTRAINT inventory_categories_pkey PRIMARY KEY (id),
    CONSTRAINT inventory_categories_name_key UNIQUE (name)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE admin.inventory_categories
    OWNER to postgres;

GRANT ALL ON TABLE admin.inventory_categories TO postgres;

GRANT SELECT ON TABLE admin.inventory_categories TO PUBLIC;