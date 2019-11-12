create table type
(
    id uuid not null
        constraint "PK_type"
            primary key,
    description text not null
);

alter table type owner to "SamuraiTeam";

create table food
(
    id uuid not null
        constraint "PK_food"
            primary key,
    name text not null,
    price numeric(4,2) not null,
    visible boolean not null,
    type uuid not null
        constraint food_type_fkey
            references type
            on delete restrict
);

alter table food owner to "SamuraiTeam";

create index "IX_food_type"
    on food (type);

create table muppet
(
    id uuid not null
        constraint "PK_muppet"
            primary key,
    real_name text,
    "IsAdmin" boolean default false not null
);

alter table muppet owner to "SamuraiTeam";

create table ingredient
(
    id uuid not null
        constraint "PK_ingredient"
            primary key,
    name text not null,
    price numeric(4,2)
);

alter table ingredient owner to "SamuraiTeam";

create table "order"
(
    id uuid not null
        constraint "PK_order"
            primary key,
    date timestamp,
    muppet uuid not null
        constraint order_muppet_fkey
            references muppet
            on delete cascade
);

alter table "order" owner to "SamuraiTeam";

create index "IX_order_muppet"
    on "order" (muppet);

create table food_order
(
    id uuid not null
        constraint "PK_food_order"
            primary key,
    food uuid
        constraint food_order_food_fkey
            references food
            on delete restrict,
    "order" uuid
        constraint food_order_order_fkey
            references "order"
            on delete cascade
);

alter table food_order owner to "SamuraiTeam";

create index "IX_food_order_food"
    on food_order (food);

create index "IX_food_order_order"
    on food_order ("order");

create table food_order_ingredient
(
    id uuid not null
        constraint "PK_food_order_ingredient"
            primary key,
    food_order uuid not null
        constraint food_order_ingredient_food_order_fkey
            references food_order
            on delete cascade,
    ingredient uuid not null
        constraint food_order_ingredient_ingredient_fkey
            references ingredient
            on delete cascade,
    isremoval boolean not null
);

alter table food_order_ingredient owner to "SamuraiTeam";

create index "IX_food_order_ingredient_food_order"
    on food_order_ingredient (food_order);

create index "IX_food_order_ingredient_ingredient"
    on food_order_ingredient (ingredient);



create table food_ingredient
(
    food uuid not null
        constraint food_ingredient_food_id_fk
            references food
            on delete cascade,
    ingredient uuid not null
        constraint food_ingredient_ingredient_id_fk
            references ingredient
            on delete cascade,
    constraint "PK_food_ingredient"
        primary key (food, ingredient)
);

alter table food_ingredient owner to "SamuraiTeam";

create index "IX_food_ingredient_ingredient"
    on food_ingredient (ingredient);

