<?php
class Address {
    private $phone;
    private $email;

    public function __construct($p = "012223344", $e = "sok@gmail.com") {
        $this->setPhone($p);
        $this->setEmail($e);
    }

    public function setPhone($p) {
        $this->phone = $p;
    }

    public function setEmail($e) {
        $this->email = $e;
    }

    public function getPhone() {
        return $this->phone;
    }

    public function getEmail() {
        return $this->email;
    }

    public function toString() {
        return $this->getPhone() . "   " . $this->getEmail();
    }
}
?>
