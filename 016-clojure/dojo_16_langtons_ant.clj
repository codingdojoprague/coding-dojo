(ns kata.core-test
  (:require [clojure.test :refer :all]))
 
;; Langton's Ant - http://en.wikipedia.org/wiki/Langton's_ant
;;
;; At a white square, turn 90° right, flip the color of the square, move forward one unit
;; At a black square, turn 90° left, flip the color of the square, move forward one unit
 
(def flip
  {:black :white
   :white :black})
 
(def turn-right
  {:north :east
   :west :north
   :east :south
   :south :west})
 
(def turn-left
  (comp turn-right turn-right turn-right))
 
(def direction->delta
  {:north [0 1]
   :east [1 0]
   :south [0 -1]
   :west [-1 0]})
 
(defn move-forward [direction coords]
  (map + coords (direction->delta direction)))
 
(defn turn [direction color]
  (let [turn (color {:black turn-left :white turn-right})]
    (turn direction)))
 
(defn move [color direction coords]
  (let [new-direction (turn direction color)]
    {:color (flip color)
     :direction new-direction
     :coords (move-forward new-direction coords)}))
 
 
(deftest langtons-ant
  (testing "flips color"
    (are [input expected-output]
      (= (flip input) expected-output)
      :black :white
      :white :black))
 
  (testing "turns"
    (testing "right"
      (are [input expected-output]
        (= (turn-right input) expected-output)
        :north :east
        :west :north
        :east :south
        :south :west))
 
    (testing "left"
      (are [input expected-output]
        (= (turn-left input) expected-output)
        :east :north
        :north :west
        :south :east
        :west :south)))
 
  (testing "moves forward"
    (testing "from [0 0]"
      (are [direction expected-position]
        (is (= (move-forward direction [0 0]) expected-position))
        :north [0 1]
        :east [1 0]
        :south [0 -1]
        :west [-1 0]))
 
    (testing "from [3 3]"
      (are [direction expected-position]
        (is (= (move-forward direction [3 3]) expected-position))
        :north [3 4]
        :east [4 3]
        :south [3 2]
        :west [2 3])))
 
  ;; Integration
  (testing "At a white square, turn 90° right, flip the color of the square, move forward one unit"
    (is (= (move :white :north [0 0]) {:color :black, :direction :east, :coords [1 0]})))
  (testing "At a black square, turn 90° left, flip the color of the square, move forward one unit"
    (is (= (move :black :north [0 0]) {:color :white, :direction :west, :coords [-1 0]}))))