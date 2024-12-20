﻿using e_commerce_website.Data;
using e_commerce_website.Enums;
using e_commerce_website.Helper.Order;
using e_commerce_website.Models;
using e_commerce_website.Services.Interfaces;
using e_commerce_website.ViewModel;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_website.Services
{
    public class ManageOrderService : IManageOrderService
    {
        private readonly ShopDbContext _context;
        public ManageOrderService(ShopDbContext context)
        {
            _context = context;
        }
        public Task<List<OrderViewModel>> GetAllOrderCancelled()
        {
            throw new NotImplementedException();
        }
        //
        private bool checkEnableOrder(int orderId)
        {
            var listOrderDetail = _context.orderDetails.Where(x => x.orderId == orderId)
                .Select(y => new CheckModel
                {
                    productId = y.productId,
                    quantity = y.quantity,
                }).ToList();
            var listProduct = new List<CheckModel>();
            foreach (var item in listOrderDetail)
            {
                var obj = _context.products.Where(x => x.id == item.productId).Select(y => new CheckModel
                {
                    productId = y.id,
                    quantity = y.amount,
                }).FirstOrDefault();
                if (item.quantity > obj.quantity)
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            return true;

        }
        //
        public async Task<List<OrderViewModel>> GetAllOrderNotConfirm()
        {
            var list = await _context.orders.Where(x => x.status == OrderStatus.NotConfirm).Include(e => e.OrderDetails)
                .Select(y => new OrderViewModel
                {
                    id = y.id,
                    address = y.address,
                    createDate = y.createDate,
                    deliveryDate = y.deliveryDate,
                    email = y.email,
                    guess = y.guess,
                    note = y.note,
                    feeShip = y.feeShip,
                    OrderDetails = y.OrderDetails,
                    phone = y.phone,
                    status = y.status,
                    street = y.street,
                    total = y.total,
                    user = y.user,
                    userId = y.userId.Value,
                }).ToListAsync();
            var temp = new List<OrderViewModel>();
            foreach (var item in list)
            {
                var i = new OrderViewModel
                {
                    id = item.id,
                    address = item.address,
                    createDate = item.createDate,
                    deliveryDate = item.deliveryDate,
                    email = item.email,
                    guess = item.guess,
                    note = item.note,
                    feeShip = item.feeShip,
                    OrderDetails = item.OrderDetails,
                    phone = item.phone,
                    status = item.status,
                    street = item.street,
                    total = item.total,
                    user = item.user,
                    userId = item.userId,
                    enableOrder = checkEnableOrder(item.id)
                };
                temp.Add(i);
            }
            return temp;
        }
        public async Task<List<OrderDetailViewModel>> GetOrderDetailByOrderId(int orderId)
        {
            var orderDetail = await _context.orderDetails.Where(x => x.orderId == orderId)
                .Select(y => new OrderDetailViewModel
                {
                    id = y.id,
                    order = y.order,
                    orderId = y.orderId,
                    productId = y.productId,
                    //_context.replies.Where(r => r.status == ActionStatus.Display && r.evaluationId == rs.id).ToList(),
                    product = _context.products.Include(i => i.Images)

                    .Where(z => z.id == y.productId).ToList(),
                    //product = y.product
                    quantity = y.quantity,
                    sale = y.sale,
                    unitPrice = y.unitPrice
                }).ToListAsync();
            return orderDetail;

        }
        public async Task<List<OrderViewModel>> GetAllOrderDelivering()
        {
            var list = await _context.orders.Where(x => x.status == OrderStatus.Shipping).Include(e => e.OrderDetails)
                .Select(y => new OrderViewModel
                {
                    id = y.id,
                    address = y.address,
                    createDate = y.createDate,
                    deliveryDate = y.deliveryDate,
                    email = y.email,
                    guess = y.guess,
                    note = y.note,
                    feeShip = y.feeShip,
                    OrderDetails = y.OrderDetails,
                    phone = y.phone,
                    status = y.status,
                    street = y.street,
                    total = y.total,
                    user = y.user,
                    userId = y.userId.Value,
                }).ToListAsync();
            return list;
        }

        public async Task<List<OrderViewModel>> GetAllOrderSuccess()
        {
            var list = await _context.orders.Where(x => x.status == OrderStatus.Success).Include(e => e.OrderDetails)
                .Select(y => new OrderViewModel
                {
                    id = y.id,
                    address = y.address,
                    createDate = y.createDate,
                    deliveryDate = y.deliveryDate,
                    email = y.email,
                    guess = y.guess,
                    note = y.note,
                    feeShip = y.feeShip,
                    OrderDetails = y.OrderDetails,
                    phone = y.phone,
                    status = y.status,
                    street = y.street,
                    total = y.total,
                    user = y.user,
                    userId = y.userId.Value,
                }).ToListAsync();
            return list;
        }

        public async Task<ResultOrderViewModel> confirmShippingAndSendMailBillOrder(StatusOrderRequest request)
        {
            var result = await MoveOrderStatus(request);
            return result;

        }
        //Hàm chuyển trạng thái và lấy thông tin
        private async Task<ResultOrderViewModel> MoveOrderStatus(StatusOrderRequest request)
        {
            var order = await _context.orders.Where(x => x.id == request.orderId).Include(u => u.user).FirstOrDefaultAsync();
            if (order == null)
            {
                return new ResultOrderViewModel { total = 0, customer = string.Empty, email = string.Empty, success = false }; ;
            }
            var total = order.total;
            var customer = string.IsNullOrEmpty(order.guess) ? order.user.displayname : order.guess;
            order.status = request.status;
            _context.Entry(order).State = EntityState.Modified;
            var rs = await _context.SaveChangesAsync() > 0;
            return new ResultOrderViewModel { total = total, customer = customer, email = order.email, success = rs };
        }

        public async Task<bool> CancelOrder(CancelOrderRequest request)
        {
            var arg = new StatusOrderRequest { orderId = request.orderId, status = request.status };
            var result = await MoveOrderStatus(arg);
            return result.success;
        }

        public async Task<bool> SetStatusNotConfirm(int orderId, OrderStatus status)
        {
            var arg = new StatusOrderRequest() { orderId = orderId, status = status };
            return await ChangeOrderStatus(arg);
        }
        //hàm chuyển trạng thái
        private async Task<bool> ChangeOrderStatus(StatusOrderRequest request)
        {
            var order = await _context.orders.Where(x => x.id == request.orderId).FirstOrDefaultAsync();
            if (order == null)
            {
                return false;
            }
            order.status = request.status;
            _context.Entry(order).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<OrderViewModel> GetOrderByOrderId(int orderId)
        {
            return await _context.orders.Where(x => x.id == orderId)
                .Select(y => new OrderViewModel
                {
                    id = y.id,
                    address = y.address,
                    createDate = y.createDate,
                    deliveryDate = y.deliveryDate,
                    email = y.email,
                    guess = y.guess,
                    note = y.note,
                    feeShip = y.feeShip,
                    OrderDetails = y.OrderDetails,
                    phone = y.phone,
                    status = y.status,
                    street = y.street,
                    total = y.total,
                    user = y.user,
                    userId = y.userId.Value,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> confirmSuccessOrder(StatusOrderRequest request)
        {
            bool check = false;
            using (var trans = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var changeStatus = await ChangeOrderStatus(request);
                    if (changeStatus == false)
                    {
                        return false;
                    }
                    var orderDetailList = await _context.orderDetails.Where(x => x.orderId == request.orderId).ToListAsync();
                    //var productList = await _context.products.ToListAsync();
                    foreach (var item in orderDetailList)
                    {
                        var product = await _context.products.FirstAsync(x => x.id == item.productId);
                        product.amount = product.amount - item.quantity;
                        _context.Entry(product).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }

                    await trans.CommitAsync();
                    check = true;
                }
                catch (Exception ex)
                {
                    await trans.RollbackAsync();
                    check = false;
                }
            }
            return check;
            //return await ChangeOrderStatus(request);
        }

        public async Task<List<OrderViewModel>> SearchOrder(SearchOrderRequest request)
        {

            Expression<Func<Order, bool>> expression = x => DateTime.Compare(DateTime.Parse(request.startDate), x.deliveryDate) <= 0
                && DateTime.Compare(DateTime.Parse(request.endDate), x.deliveryDate) >= 0;

            var query = _context.orders.AsQueryable();
            if (!string.IsNullOrEmpty(request.keyWord))
            {
                query = query.Where(x => (!string.IsNullOrEmpty(x.guess) ? x.guess : x.user.displayname).Contains(request.keyWord)
                || x.phone.Contains(request.keyWord) || x.email.Contains(request.keyWord));
            }
            if (!string.IsNullOrEmpty(request.startDate) && !string.IsNullOrEmpty(request.endDate))
            {
                query = query.Where(expression);
            }
            //TH là OrderNotConfirm
            if (request.status == 0)
            {
                var list = await query.Where(x => x.status == OrderStatus.NotConfirm).Include(e => e.OrderDetails)
                .Select(y => new OrderViewModel
                {
                    id = y.id,
                    address = y.address,
                    createDate = y.createDate,
                    deliveryDate = y.deliveryDate,
                    email = y.email,
                    guess = y.guess,
                    note = y.note,
                    feeShip = y.feeShip,
                    OrderDetails = y.OrderDetails,
                    phone = y.phone,
                    status = y.status,
                    street = y.street,
                    total = y.total,
                    user = y.user,
                    userId = y.userId.Value,
                }).ToListAsync();
                var temp = new List<OrderViewModel>();
                foreach (var item in list)
                {
                    var i = new OrderViewModel
                    {
                        id = item.id,
                        address = item.address,
                        createDate = item.createDate,
                        deliveryDate = item.deliveryDate,
                        email = item.email,
                        guess = item.guess,
                        note = item.note,
                        feeShip = item.feeShip,
                        OrderDetails = item.OrderDetails,
                        phone = item.phone,
                        status = item.status,
                        street = item.street,
                        total = item.total,
                        user = item.user,
                        userId = item.userId,
                        enableOrder = checkEnableOrder(item.id)
                    };
                    temp.Add(i);
                }
                return temp;
            }
            return await query.Where(z => z.status == request.status).Select(r => new OrderViewModel
            {
                id = r.id,
                address = r.address,
                createDate = r.createDate,
                deliveryDate = r.deliveryDate,
                email = r.email,
                guess = r.guess,
                note = r.note,
                feeShip = r.feeShip,
                OrderDetails = r.OrderDetails,
                phone = r.phone,
                status = r.status,
                street = r.street,
                total = r.total,
                user = r.user,
                userId = r.userId.Value,
            }
            ).ToListAsync();
        }
    }
}
