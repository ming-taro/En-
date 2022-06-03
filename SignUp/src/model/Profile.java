package model;

import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JPasswordField;
import javax.swing.JTextField;

public class Profile {
	private String id;
	private String password;
	private String name;
	private String birth;
	private String sex;
	private String zipCode;
	private String roadNameAddress;
	private String detailAddress;
	private String phoneNumber;
	private String email;
	
	public Profile(String id, String password, String name, 
			String birth, String sex, String zipCode, String roadNameAddress,
			String detailAddress, String phoneNumber, String email) {
		this.id = id;
		this.password = password;
		this.name = name;
		this.birth = birth;
		this.sex = sex;
		this.zipCode = zipCode;
		this.roadNameAddress = roadNameAddress;
		this.detailAddress = detailAddress;
		this.phoneNumber = phoneNumber;
		this.email = email;
	}
	
	public String getId() {
		return id;
	}
	public String getPassword() {
		return password;
	}
	public String getName() {
		return name;
	}
	public String getBirth() {
		return birth;
	}
	public String getSex() {
		return sex;
	}
	public String getZipCode() {
		return zipCode;
	}
	public String getRoadNameAddress() {
		return roadNameAddress;
	}
	public String getDetailAddress() {
		return detailAddress;
	}
	public String getPhoneNumber() {
		return phoneNumber;
	}
	public String getEmail() {
		return email;
	}
	
	public void setPassword(String password) {
		this.password = password;
	}
	
	public void setZipCode(String zipCode) {
		this.zipCode = zipCode;
	}
	
	public void setRoadNameAddress(String roadNameAddress) {
		this.roadNameAddress = roadNameAddress;
	}
	
	public void setDetailAddress(String detailAddress) {
		this.detailAddress = detailAddress;
	}
	
	public void setEmail(String email) {
		this.email = email;
	}
}
