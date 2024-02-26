<?php

class Score {
    private $sub1;
    private $sub2;
    private $sub3;
    private $sub4;
    private $sub5;

    public function __construct($s1 = 100, $s2 = 100, $s3 = 80, $s4 = 90, $s5 = 80) {
        $this->setSub1($s1);
        $this->setSub2($s2);
        $this->setSub3($s3);
        $this->setSub4($s4);
        $this->setSub5($s5);
    }

    public function setSub1($i) {
        $this->sub1 = $i;
    }

    public function setSub2($i) {
        $this->sub2 = $i;
    }

    public function setSub3($i) {
        $this->sub3 = $i;
    }

    public function setSub4($i) {
        $this->sub4 = $i;
    }

    public function setSub5($i) {
        $this->sub5 = $i;
    }

    public function getSub1() {
        return $this->sub1;
    }

    public function getSub2() {
        return $this->sub2;
    }

    public function getSub3() {
        return $this->sub3;
    }

    public function getSub4() {
        return $this->sub4;
    }

    public function getSub5() {
        return $this->sub5;
    }

    public function getTotal() {
        return $this->getSub1() +
            $this->getSub2() +
            $this->getSub3() +
            $this->getSub4() +
            $this->getSub5();
    }

    public function getGrade() {
        $average = $this->getTotal() / 5;
        $grade = "";
        if ($average >= 85) $grade = "A";
        elseif ($average >= 80) $grade = "B";
        elseif ($average >= 70) $grade = "C";
        elseif ($average >= 60) $grade = "D";
        elseif ($average >= 50) $grade = "E";
        else $grade = "F";
        return $grade;
    }

    public function toString() {
        return
            $this->getSub1() . "   " .
            $this->getSub2() . "   " .
            $this->getSub3() . "   " .
            $this->getSub4() . "   " .
            $this->getSub5() . "   " .
            $this->getTotal() . "   " .
            $this->getGrade();
    }
}

?>
