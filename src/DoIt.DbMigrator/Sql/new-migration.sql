CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;

CREATE TABLE to_do_lists (
    id uuid NOT NULL,
    title text NOT NULL,
    CONSTRAINT pk_to_do_lists PRIMARY KEY (id)
);

CREATE TABLE to_do_item (
    id uuid NOT NULL,
    title text NOT NULL,
    done boolean NOT NULL,
    note text NULL,
    to_do_list_id uuid NULL,
    CONSTRAINT pk_to_do_item PRIMARY KEY (id),
    CONSTRAINT fk_to_do_item_to_do_lists_to_do_list_id FOREIGN KEY (to_do_list_id) REFERENCES to_do_lists (id) ON DELETE RESTRICT
);

CREATE INDEX ix_to_do_item_title ON to_do_item (title);

CREATE INDEX ix_to_do_item_to_do_list_id ON to_do_item (to_do_list_id);

CREATE INDEX ix_to_do_lists_title ON to_do_lists (title);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20210411143432_NewMigration', '5.0.5');

COMMIT;

