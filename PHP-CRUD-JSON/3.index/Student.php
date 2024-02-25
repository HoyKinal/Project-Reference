<?php

include "Address.php";
include "Score.php";

class Student
{
    private $id;
    private $name;
    private $gender;
    private $year;
    private $address;
    private $score;

    public function __construct($i = 1, $n = "sok san", $g = "M", $y = 4)
    {
        $this->setId($i);
        $this->setName($n);
        $this->setGender($g);
        $this->setYear($y);
        $this->setAddress();  // No need to pass parameters here
        $this->setScore();    // No need to pass parameters here
    }

    public function setAddress($phone = "", $email = "")
    {
        $this->address = new Address($phone, $email);
    }

    public function setScore($sub1 = 0, $sub2 = 0, $sub3 = 0, $sub4 = 0, $sub5 = 0)
    {
        $this->score = new Score($sub1, $sub2, $sub3, $sub4, $sub5);
    }

    public function setId($i)
    {
        $this->id = $i;
    }

    public function setName($n)
    {
        $this->name = $n;
    }

    public function setGender($g)
    {
        $this->gender = $g;
    }

    public function setYear($y)
    {
        $this->year = $y;
    }

    public function getId()
    {
        return $this->id;
    }

    public function getName()
    {
        return $this->name;
    }

    public function getGender()
    {
        return $this->gender;
    }

    public function getYear()
    {
        return $this->year;
    }

    public function getAddress()
    {
        return $this->address;
    }

    public function getScore()
    {
        return $this->score;
    }

    public function toArray()
    {
        return [
            "id" => $this->getId(),
            "name" => $this->getName(),
            "gender" => $this->getGender(),
            "year" => $this->getYear(),
            "address" => [
                "phone" => $this->getAddress()->getPhone(),
                "email" => $this->getAddress()->getEmail(),
            ],
            "score" => [
                "sub1" => $this->getScore()->getSub1(),
                "sub2" => $this->getScore()->getSub2(),
                "sub3" => $this->getScore()->getSub3(),
                "sub4" => $this->getScore()->getSub4(),
                "sub5" => $this->getScore()->getSub5(),
                "total" => $this->getScore()->getTotal(),
                "grade" => $this->getScore()->getGrade(),
            ],
        ];
    }
}

?>
