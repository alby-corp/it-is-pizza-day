create table ingredient
(
  id      uuid not null constraint ingredient_pkey primary key,
  name text not null,
  price numeric(4,2)
);

create table type
(
	id uuid not null constraint type_pkey primary key,
	description text not null,
	priority integer
);

create table food
(
  id uuid not null constraint food_pkey primary key,
  name text not null,
  price numeric(4,2) default 0 not null,
  visible boolean default true not null,
  type uuid default '00000000-0000-0000-0000-000000000000'::uuid
);

create table food_ingredient
(
	food uuid not null constraint food_ingredient_food_id_fk references food,
	ingredient uuid not null constraint food_ingredient_ingredient_id_fk references ingredient,
	constraint food_ingredient_food_ingredient_pk primary key (food, ingredient)
);

create table muppet
(
  id uuid not null constraint muppet_pkey primary key,
  real_name text
);

create table "order"
(
  id uuid not null constraint order_pkey primary key,
  muppet_old uuid,
  date timestamp,
  muppet uuid default '00000000-0000-0000-0000-000000000000'::uuid not null constraint order_muppet_fkey references muppet
);

create table food_order
(
  id uuid not null constraint food_order_pkey primary key,
  food uuid constraint food_order_food_fkey references food,
  "order" uuid constraint food_order_order_fkey references "order"
);

create table food_order_ingredient
(
  id uuid not null constraint food_order_ingredient_pkey primary key,
  food_order uuid not null constraint food_order_ingredient_food_order_fkey references food_order,
  ingredient uuid not null constraint food_order_ingredient_ingredient_fkey references ingredient,
  isremoval boolean not null
);