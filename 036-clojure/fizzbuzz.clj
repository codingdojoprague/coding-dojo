(ns coding-dojo)

(defn fizz-buzz-number [x]
  (cond
   (= 0 (mod x 3) (mod x 5)) :fizz/buzz
   (= 0 (mod x 3)) :fizz
   (= 0 (mod x 5)) :buzz
   :else x))

(defn fizz-buzz [n]
  (vec (map fizz-buzz-number (range 1 (inc n)))))

(defn fizz-buzz-threading [n]
  (->> (range 1 (inc n))
       (map fizz-buzz-number)
       vec))

(do
  (assert (= (fizz-buzz 1) [1]))
  (assert (= (fizz-buzz 2) [1 2]))
  (assert (= (fizz-buzz 3) [1 2 :fizz]))
  (assert (= (fizz-buzz 4) [1 2 :fizz 4]))
  (assert (= (fizz-buzz 5) [1 2 :fizz 4 :buzz]))
  (assert (= (fizz-buzz 6) [1 2 :fizz 4 :buzz :fizz]))
  (assert (= (fizz-buzz 7) [1 2 :fizz 4 :buzz :fizz 7]))
  (assert (= (fizz-buzz 8) [1 2 :fizz 4 :buzz :fizz 7 8]))
  (assert (= (fizz-buzz 15) [1 2 :fizz 4 :buzz :fizz 7 8 :fizz :buzz 11 :fizz 13 14 :fizz/buzz]))
  :success)
