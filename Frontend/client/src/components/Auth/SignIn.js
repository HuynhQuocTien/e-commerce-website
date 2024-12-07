import React, { Component } from "react";
import { Form, Input, Button, Checkbox, Spin, Row, Col, message } from "antd";
import { UserOutlined, LockOutlined } from "@ant-design/icons";
import { Link } from "react-router-dom";
import { FaFacebookF, FaGoogle, FaGithub } from "react-icons/fa";
import { connect } from "react-redux";
import FacebookLogin from "react-facebook-login";
// import GoogleLogin from "react-google-login";
import { GoogleOAuthProvider, GoogleLogin } from "@react-oauth/google";
import GitHubLogin from "react-github-login";
import { gapi } from "gapi-script";
import "./styleLogin.css";

class SignIn extends Component {
  constructor() {
    super();
    this.onFinish = this.onFinish.bind(this);
  }

  formLogin = Form.create();
  componentDidMount() {
    gapi.load("client:auth2", () => {
      gapi.client.init({
        clientId:
          "124352557332-vfcoofjdmcrmft6n6i6vjsspp61vg5n6.apps.googleusercontent.com",
        scope: "email",
      });
    });
  }
  onFinish = (values) => {
    this.props.onSignIn(values);
    this.formLogin.resetFields();
  };
  responseFacebook = (response) => {
    console.log("FB Response:", response);
    if (response.status) {
      message.warning("LOGIN FACEBOOK FAILED");
    } else {
      const body = {
        name: response.name,
        email: response.email,
        avatar: response.picture?.data?.url || null,
        userId: response.id,
      };
      this.props.loginFb(body);
    }
  };

  // responseGoogle = (response) => {
  //   console.log("Google Response:", response);
  //   if (response.error) {
  //     if (response.error === "popup_closed_by_user") {
  //       message.warning("Google login was cancelled. Please try again.");
  //     } else {
  //       message.warning("LOGIN GOOGLE FAILED");
  //     }
  //   } else {
  //     console.log(response);
  //     const body = {
  //       tokenId: response.credential,
  //     };
  //     this.props.loginGoogle(body);
  //   }
  // };

  responseGitHub = (response) => {
    console.log("GitHub Response:", response);
    // const body = {
    //   token: response.code,
    // };
    // this.props.loginGitHub(body);
  };

  render() {
    return (
      <GoogleOAuthProvider clientId="124352557332-vfcoofjdmcrmft6n6i6vjsspp61vg5n6.apps.googleusercontent.com">
        <Spin spinning={this.props.isLoading} tip="ĐĂNG NHẬP" size="large">
          <Form
            form={this.formLogin}
            name="normal_login"
            className="login-form"
            initialValues={{
              username: "",
              password: "",
              remember: false,
            }}
            onFinish={this.onFinish}
          >
            <Form.Item
              name="username"
              rules={[
                {
                  required: true,
                  message: "Xin vui lòng nhập tài khoản!",
                },
                {
                  whitespace: true,
                  message: "Tài khoản toàn là dấu cách!",
                },
              ]}
            >
              <Input
                prefix={<UserOutlined className="site-form-item-icon" />}
                placeholder="Username"
              />
            </Form.Item>
            <Form.Item
              name="password"
              rules={[
                {
                  required: true,
                  message: "Xin vui lòng nhập mật khẩu!",
                },
                {
                  min: 10,
                  message: "Mật khẩu phải có 10 ký tự trở lên!",
                },
                {
                  whitespace: true,
                  message: "Mật khẩu toàn là dấu cách!",
                },
              ]}
            >
              <Input.Password
                prefix={<LockOutlined className="site-form-item-icon" />}
                type="password"
                placeholder="Password"
              />
            </Form.Item>
            <Form.Item>
              <Checkbox>Nhớ tài khoản?</Checkbox>
              <Link className="login-form-forgot" to="#">
                Quên mật khẩu
              </Link>
            </Form.Item>
            <Form.Item>
              <Button
                type="primary"
                htmlType="submit"
                className="login-form-button"
              >
                ĐĂNG NHẬP
              </Button>
            </Form.Item>
            <Row className="row-social-buttons">
              <Col>
                <FacebookLogin
                  appId="646768039528160"
                  fields="name,email,picture"
                  callback={this.responseFacebook}
                  cssClass="custom-facebook-button"
                  textButton="Facebook"
                  icon={<FaFacebookF style={{ marginRight: 8 }} />}
                />
              </Col>
              <Col>
                <GoogleLogin
                  clientId="124352557332-vfcoofjdmcrmft6n6i6vjsspp61vg5n6.apps.googleusercontent.com"
                  fields="name,email,picture"
                  onSuccess={this.props.handleGoogleResponse} // Use the passed handler
                  onFailure={this.props.handleGoogleResponse}
                  cookiePolicy={"single_host_origin"}
                  render={(renderProps) => (
                    <Button
                      onClick={renderProps.onClick}
                      disabled={renderProps.disabled}
                      icon={<FaGoogle style={{ marginRight: 8 }} />}
                      className="custom-google-button"
                    >
                      Google
                    </Button>
                  )}
                />
              </Col>
            </Row>
          </Form>
        </Spin>
      </GoogleOAuthProvider>
    );
  }
}

const mapStateToProps = (state) => ({
  isLoading: state.auth.isLoadingLogin,
});

export default connect(mapStateToProps, null)(SignIn);
