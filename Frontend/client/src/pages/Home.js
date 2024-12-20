import React, { Component } from 'react'
import Banner from '../components/banner/Banner';

import {Row, Col} from 'antd';
import {EnvironmentOutlined, WhatsAppOutlined, MailOutlined} from '@ant-design/icons';

import axiosInstance from '../utils/axiosInstance';
import ListProducts from '../components/products/ListProducts';
import { FacebookProvider, Page } from 'react-facebook';
import { Map, GoogleApiWrapper, InfoWindow, Marker } from 'google-maps-react';
import { AddressMap } from '../components/common/AddressMap';

const intro = {
    padding: 10,
}

class Home extends Component {
    constructor(props){
        super(props);
        this.state = {
            topViewProducts: [],
            mainProducts: [],
            itemCount: 4,
            isLoadingTopViewProduct: true,
            isLoadingMainProduct: true,
            //
            isViewMoreMainProduct: false,
            pageCurrent: 1,
            pageSize: 8,
            totalPage: 0,
        }
    }
    componentDidMount(){
        window.scrollTo(0, 0);
        axiosInstance('Product/products-top-view-count/false')
        .then(res => {
            this.setState({
                topViewProducts: [...res.data],
                isLoadingTopViewProduct: false,
            })
        })
        .catch(err => console.log(err + ''));
        axiosInstance(`Product/get-all-products/${this.state.itemCount}`)
        .then(res => {
            console.log(res.data.data);
            this.setState({
                mainProducts: [...res.data.data],
                
                isLoadingMainProduct: false,
            })
        })
        .catch(err => console.log(err + ''))
        
    }
    handleClickViewMore(value){
        this.setState({
            
            isLoadingTopViewProduct: true,
        })
        axiosInstance('Product/products-top-view-count/true')
        .then(res => {
            this.setState({
                topViewProducts: [...res.data],
                isLoadingTopViewProduct: false,
            })
        })
        .catch(err => console.log(err + ''))
    }
    handleClickViewMoreForMainProduct(value){
        this.setState({
            isViewMoreMainProduct: true,
            isLoadingMainProduct: true,
        })
        axiosInstance(`Product/get-all-products/${this.state.itemCount + 4}`)
        .then(res => {
            this.setState({
                mainProducts: [...res.data.data],
                totalPage: res.data.totalColumns,
                isLoadingMainProduct: false,
            })
        })
        .catch(err => console.log(err + ''))
    }
    //
    async handleChangePage(page, pageSize){
        let data = await axiosInstance('Product/Paging', 'POST',{pageCurrent: page, pageSize: pageSize})
        .then(res => res.data);
        this.setState({
            pageCurrent: page,
            pageSize: pageSize,
            mainProducts: data,
        })
    }
    render(){
        const {pageCurrent, pageSize, totalPage} = this.state;
        return (
            
            <div style={{background: '#f7f7f7'}}>
                <Banner></Banner>
                
                <ListProducts title="NHỮNG SẢN PHẨM XEM NHIỀU" onClickViewMore={this.handleClickViewMore.bind(this)} 
                loading={this.state.isLoadingTopViewProduct} products={this.state.topViewProducts}></ListProducts>
                
                <ListProducts title="NHỮNG SẢN PHẨM KHÁC" onClickViewMore={this.handleClickViewMoreForMainProduct.bind(this)} 
                loading={this.state.isLoadingMainProduct} products={this.state.mainProducts}
                isViewMore={this.state.isViewMoreMainProduct}
                pageCurrent={pageCurrent} pageSize={pageSize} totalPage={totalPage}
                onChangePage={this.handleChangePage.bind(this)}
                ></ListProducts>
                <br></br>

                <div style={{maxWidth: '75%', margin: '20px auto'}}>
                <Row>
                    <Col lg={{span: 9}}>
                        <div style={intro}>
                            <h5 style={{color: '#fa8c16'}}>HỆ THỐNG CỬA HÀNG</h5>
                            <p><EnvironmentOutlined /> 273 Đ. An Dương Vương, Phường 3, Quận 5, Hồ Chí Minh</p>
                            <p><WhatsAppOutlined /> +(84) 374691347</p>
                            <p><MailOutlined /> quoctien01062003@gmail.com</p>
                        </div>
                    </Col>
                    <Col lg={{span: 7}}>
                        <div style={intro}>
                            <h5 style={{color: '#fa8c16'}}>FAN PAGE</h5>
                              <FacebookProvider appId="1182923419526480">
                                  <Page href="https://www.facebook.com/hqtienn.1623/"/>
                              </FacebookProvider>
                              
                          </div>
                      </Col>
                      <Col lg={{span: 8}}>
                          <div style={intro}>
                              <h5 style={{color: '#fa8c16'}}>BẢN ĐỒ</h5>
                              <AddressMap></AddressMap>
                          </div>
                      </Col>
                  </Row>
                  </div>
                  
              </div>
          )
      }
      
}
export default Home;
