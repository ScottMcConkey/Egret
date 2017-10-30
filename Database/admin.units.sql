-- Table: admin.units

-- DROP TABLE admin.units;

CREATE TABLE admin.units
(
    id integer NOT NULL DEFAULT nextval('admin.units_id_seq'::regclass),
    name text COLLATE pg_catalog."default" NOT NULL,
    abbreviation text COLLATE pg_catalog."default" NOT NULL,
    sortorder integer,
    CONSTRAINT units_pkey PRIMARY KEY (id),
    CONSTRAINT units_sortorder_key UNIQUE (sortorder)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE admin.units
    OWNER to postgres;

GRANT ALL ON TABLE admin.units TO postgres;

GRANT SELECT ON TABLE admin.units TO PUBLIC;