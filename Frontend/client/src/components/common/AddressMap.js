import React from 'react';
import { Row, Col } from 'antd';
 const AddressMap=()=>{
    return (
        <Row>
            <Col lg={{span: 24}}>
            <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d15678.076326623212!2d106.68394002589488!3d10.77149855971109!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752f1b7c3ed289%3A0xa06651894598e488!2zVHLGsOG7nW5nIMSQ4bqhaSBo4buNYyBTw6BpIEfDsm4!5e0!3m2!1svi!2s!4v1731402420249!5m2!1svi!2s" width="100%" height="350"  style={{border:0}}  aria-hidden="false" ></iframe>
            </Col>
        </Row>
    );
 }
 export {AddressMap}