CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;

CREATE TABLE to_do_lists (
    id uuid NOT NULL,
    title text NOT NULL,
    date_created timestamp with time zone NOT NULL,
    date_modified timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    date_deleted timestamp with time zone NULL,
    deleted_by text NULL,
    CONSTRAINT pk_to_do_lists PRIMARY KEY (id)
);

CREATE TABLE todo_item (
    id uuid NOT NULL,
    title text NOT NULL,
    done boolean NOT NULL,
    note text NULL,
    todo_list_id uuid NOT NULL,
    date_created timestamp with time zone NOT NULL,
    date_modified timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    date_deleted timestamp with time zone NULL,
    deleted_by text NULL,
    CONSTRAINT pk_todo_item PRIMARY KEY (id),
    CONSTRAINT fk_todo_item_to_do_lists_todo_list_id FOREIGN KEY (todo_list_id) REFERENCES to_do_lists (id) ON DELETE CASCADE
);

CREATE INDEX ix_to_do_lists_title ON to_do_lists (title);

CREATE INDEX ix_todo_item_title ON todo_item (title);

CREATE INDEX ix_todo_item_todo_list_id ON todo_item (todo_list_id);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20210621060619_Initial', '5.0.7');

COMMIT;

