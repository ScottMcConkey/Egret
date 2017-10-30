-- Table: admin.currency_types

-- DROP TABLE admin.currency_types;

CREATE TABLE admin.currency_types
(
    id integer NOT NULL,
    name text COLLATE pg_catalog."default" NOT NULL,
    symbol text COLLATE pg_catalog."default" NOT NULL,
    sortorder integer NOT NULL DEFAULT nextval('admin.currency_types_sortorder_seq'::regclass),
    abbreviation text COLLATE pg_catalog."default",
    CONSTRAINT currency_types_pkey PRIMARY KEY (id),
    CONSTRAINT currency_types_abbreviation_key UNIQUE (abbreviation)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE admin.currency_types
    OWNER to postgres;