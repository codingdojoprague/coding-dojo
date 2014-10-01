-module( langton ).
-include_lib( "eunit/include/eunit.hrl" ).
-export( [ step/2 ] ).

%% Install erlang from http://www.erlang.org/ (or use your package manager)

%% In order to run tests use:
%% $ erlc langton.erl && erl -noshell -eval "eunit:test(langton, [verbose])" -s init stop

flip( white ) -> black;
flip( black ) -> white.

rotate( black, east ) -> north;
rotate( black, south ) -> east;
rotate( black, west ) -> south;
rotate( black, north ) -> west;

rotate( white, north ) -> east;
rotate( white, east ) -> south;
rotate( white, south ) -> west;
rotate( white, west ) -> north.

move( { X, Y }, north ) -> { X, Y + 1 };
move( { X, Y }, east ) -> { X + 1, Y };
move( { X, Y }, south ) -> { X, Y - 1 };
move( { X, Y }, west ) -> { X - 1, Y }.


flip_test() ->
	?assertMatch( white, flip( black ) ),
	?assertMatch( black, flip( white ) ).

rotates_left_on_black_test() ->
	?assertMatch( north, rotate( black, east ) ),
	?assertMatch( east, rotate( black, south ) ),
	?assertMatch( south, rotate( black, west ) ),
	?assertMatch( west, rotate( black, north ) ).

rotates_right_on_white_test() ->
	?assertMatch( east, rotate( white, north ) ),
	?assertMatch( south, rotate( white, east ) ),
	?assertMatch( west, rotate( white, south ) ),
	?assertMatch( north, rotate( white, west ) ).

moves_in_direction_test() ->
	?assertMatch( { 1, 1 }, move( { 1, 0 }, north ) ),
	?assertMatch( { 1, 0 }, move( { 0, 0 }, east ) ),
	?assertMatch( { 0, 0 }, move( { 0, 1 }, south ) ),
	?assertMatch( { 0, 0 }, move( { 1, 0 }, west ) ),
	?assertMatch( { 1, 1 }, move( { 2, 1 }, west ) ).

%% At a white square, turn 90° right, flip the color of the square, move forward one unit
%% At a black square, turn 90° left, flip the color of the square, move forward one unit

step( Color, { Position, Orientation } ) ->
	NewOrientation = rotate( Color, Orientation ),
	{ flip( Color ), { move( Position, NewOrientation ), NewOrientation  } }.


ant_obeys_rules_test() ->
	?assertMatch(
		{ _Color = white, _Ant = { { 0, 0 }, north } },
		step( black, { { 0, -1 }, east } ) ),
	?assertMatch(
		{ black, { { 0, 0 }, south } },
		step( white, { { 0, 1 }, east  } ) ).
