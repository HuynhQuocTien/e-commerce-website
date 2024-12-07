import React, { Component } from "react";
import { Modal, Input, Form, Upload, Select, Row, Col, Button } from "antd";
import { PlusOutlined } from "@ant-design/icons";
import axiosInstance from "../../../utils/axiosInstance";
import { colors } from "../../../utils/colors";
import { sizes } from "../../../utils/sizes";
const { TextArea } = Input;
const { Option } = Select;

const config = {
  labelCol: {
    span: 8,
  },
  wrapperCol: {
    span: 16,
  },
};

export default class ModalProduct extends Component {
  constructor(props) {
    super(props);
    this.state = {
      categories: [],
      providers: [],
      imageList: [],
      colors: [],
      sizes: [],
    };
  }

  componentDidMount() {
    const { images } = this.props.data;

    this.setState({
      imageList: images
        ? images.map((ele) => {
            return {
              uid: ele.id,
              url: ele.urlImage,
              status: ele.status,
              productId: ele.productId,
            };
          })
        : [],
    });

    const tempColors = Object.keys(colors).map(function (key) {
      return { id: Number(key), value: colors[key] };
    });
    this.setState({
      colors: [...tempColors],
    });

    const tempSizes = Object.keys(sizes).map(function (key) {
      return { id: Number(key), value: sizes[key] };
    });

    this.setState({
      sizes: [...tempSizes],
    });

    axiosInstance("ManageCategory", "GET").then((res) => {
      this.setState({
        categories: [...res.data],
      });
    });
    axiosInstance("ManageProvider", "GET").then((res) => {
      this.setState({
        providers: [...res.data],
      });
    });
  }

  componentWillUnmount() {
    this.setState({
      categories: [],
      providers: [],
      imageList: [],
      colors: [],
      sizes: [],
    });
  }

  handleCancel() {
    this.props.onCancel(false);
  }

  handleChange({ fileList }) {
    this.setState({ imageList: fileList });
  }

  handleUpload = (file) => {
    const upload = `https://localhost:5001/api/ManageProduct/image`;
    return upload;
  };

  handleSubmitForm(values) {
    const { imageList } = this.state;
    const { data } = this.props;
    console.log(values);
    this.props.onSubmitForm({ id: data.id, ...values, images: [...imageList] });
  }

  handleRemoveImage = (file) => {
    console.log(file);
  };

  render() {
    const { categories, providers, colors, sizes } = this.state;
    const { data, visible } = this.props;

    const uploadButton = (
      <div>
        <PlusOutlined />
        <div className="ant-upload-text">Upload</div>
      </div>
    );

    return (
      <Modal
        width={800}
        title={data.id ? "Cập nhập sản phẩm" : "Thêm sản phẩm"}
        visible={visible}
        onCancel={this.handleCancel.bind(this)}
        footer={null}
      >
        <div>
          <Form
            onFinish={this.handleSubmitForm.bind(this)}
            initialValues={{
              name: data.name,
              importPrice: data.importPrice,
              price: data.price,
              sale: data.sale,
              color: data.color,
              size: data.size,
              categoryId: data.categoryId,
              providerId: data.providerId,
              description: data.description
                ? data.description.split(";").join("\n")
                : "",
              amount: data.amount,
              viewCount: data.viewCount,
            }}
          >
            <Row>
              <Col span={18} offset={3}>
                <Form.Item>
                  <Upload
                    onChange={this.handleChange.bind(this)}
                    listType="picture-card"
                    fileList={this.state.imageList}
                    action={this.handleUpload.bind(this)}
                    onRemove={this.handleRemoveImage.bind(this)}
                  >
                    {this.state.imageList.length >= 5 ? null : uploadButton}
                  </Upload>
                </Form.Item>
              </Col>
            </Row>

            <Row gutter={{ xs: 8, sm: 16, md: 24, lg: 32 }}>
              <Col className="gutter-row" span={12}>
                <Form.Item
                  name="name"
                  label="Tên sản phẩm"
                  {...config}
                  rules={[
                    {
                      required: true,
                      message: "Tên sản phẩm không thể để trống!",
                    },
                  ]}
                >
                  <Input type="text" placeholder="Tên sản phẩm" />
                </Form.Item>
                <Form.Item
                  name="importPrice"
                  label="Giá nhập (VNĐ)"
                  {...config}
                  rules={[
                    { required: true, message: "Giá nhập không thể để trống!" },
                    { pattern: /^[0-9]+$/, message: "Giá nhập phải là số!" },
                  ]}
                >
                  <Input type="text" placeholder="Giá nhập" />
                </Form.Item>
                <Form.Item
                  name="price"
                  label="Giá bán (VNĐ)"
                  {...config}
                  rules={[
                    { required: true, message: "Giá bán không thể để trống!" },
                    { pattern: /^[0-9]+$/, message: "Giá bán phải là số!" },
                  ]}
                >
                  <Input type="text" placeholder="Giá bán" />
                </Form.Item>
                <Form.Item
                  name="sale"
                  label="Giảm giá (%)"
                  {...config}
                  rules={[
                    { required: true, message: "Giảm giá không thể để trống!" },
                    { pattern: /^[0-9]+$/, message: "Giảm giá phải là số!" },
                  ]}
                >
                  <Input type="text" placeholder="Giảm giá" />
                </Form.Item>
                <Form.Item name="viewCount" label="Lượng xem" {...config}>
                  <Input disabled type="text" placeholder="Lượng xem" />
                </Form.Item>
              </Col>

              <Col className="gutter-row" span={12}>
                <Form.Item
                  name="amount"
                  label="Số lượng"
                  {...config}
                  rules={[
                    { required: true, message: "Số lượng không thể để trống!" },
                    { pattern: /^[0-9]+$/, message: "Số lượng phải là số!" },
                  ]}
                >
                  <Input type="text" placeholder="Số lượng" />
                </Form.Item>
                <Form.Item
                  name="categoryId"
                  label="Danh mục"
                  {...config}
                  rules={[
                    { required: true, message: "Danh mục không thể để trống!" },
                  ]}
                >
                  <Select placeholder="Danh mục">
                    {categories.map((ele) => {
                      return (
                        <Option key={ele.id} value={ele.id}>
                          {ele.name}
                        </Option>
                      );
                    })}
                  </Select>
                </Form.Item>
                <Form.Item
                  name="providerId"
                  label="Nhà cung cấp"
                  {...config}
                  rules={[
                    {
                      required: true,
                      message: "Nhà cung cấp không thể để trống!",
                    },
                  ]}
                >
                  <Select placeholder="Nhà cung cấp">
                    {providers.map((ele) => {
                      return (
                        <Option key={ele.id} value={ele.id}>
                          {ele.name}
                        </Option>
                      );
                    })}
                  </Select>
                </Form.Item>
                <Form.Item
                  name="size"
                  label="Size"
                  {...config}
                  rules={[
                    { required: true, message: "Size không thể để trống!" },
                  ]}
                >
                  <Select placeholder="Size">
                    {sizes.map((ele) => {
                      return (
                        <Option key={ele.id} value={ele.id}>
                          {ele.value}
                        </Option>
                      );
                    })}
                  </Select>
                </Form.Item>
                <Form.Item
                  name="color"
                  label="Color"
                  {...config}
                  rules={[
                    { required: true, message: "Màu sắc không thể để trống!" },
                  ]}
                >
                  <Select placeholder="Màu sắc">
                    {colors.map((ele) => {
                      return (
                        <Option
                          style={{ color: `${ele.value}` }}
                          key={ele.id}
                          value={ele.id}
                        >
                          {ele.value}
                        </Option>
                      );
                    })}
                  </Select>
                </Form.Item>
              </Col>
            </Row>

            <Form.Item
              name="description"
              label="Mô tả"
              labelCol={{ span: 4 }}
              wrapperCol={{ span: 20 }}
            >
              <TextArea placeholder="Mô tả" />
            </Form.Item>

            <Row>
              <Col span={12} offset={6}>
                <Form.Item>
                  <Button htmlType="submit" size="large" type="primary" block>
                    Submit
                  </Button>
                </Form.Item>
              </Col>
            </Row>
          </Form>
        </div>
      </Modal>
    );
  }
}
