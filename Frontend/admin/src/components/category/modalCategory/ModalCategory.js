import React, { Component } from "react";
import { Modal, Input, Button, Form } from "antd";

export default class ModalCategory extends Component {
  handleSubmit = (values) => {
    const { data } = this.props;
    this.props.onSubmitForm({ id: data.id, ...values });
  };

  handleCancel = () => {
    if (this.props.onCancel) {
      this.props.onCancel(false); // Pass `false` or handle based on your specific requirements
    }
  };

  render() {
    const { data, visible } = this.props;

    return (
      <Modal
        title={data.id ? "Cập nhập danh mục" : "Thêm danh mục"}
        visible={visible}
        onCancel={this.handleCancel}
        footer={null}
      >
        <div>
          <Form
            onFinish={this.handleSubmit}
            initialValues={{
              generalityName: data.generalityName,
              name: data.name,
            }}
          >
            <Form.Item
              name="generalityName"
              label="Tên chung"
              rules={[
                { required: true, message: "Tên chung không được để trống" },
              ]}
            >
              <Input
                allowClear
                size="large"
                placeholder="Tên chung (Quần áo, ...)"
              />
            </Form.Item>
            <Form.Item
              name="name"
              label="Tên chi tiết"
              rules={[
                { required: true, message: "Tên chi tiết không được để trống" },
              ]}
            >
              <Input
                allowClear
                size="large"
                placeholder="Tên chi tiết (Áo sơ mi, Quần Jeans,...)"
              />
            </Form.Item>
            <div style={{ textAlign: "end" }}>
              <Button htmlType="submit" type="primary">
                Submit
              </Button>
            </div>
          </Form>
        </div>
      </Modal>
    );
  }
}
