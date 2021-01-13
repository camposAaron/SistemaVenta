
/*Tablas Base de Datos SIFFS*/
--
Create table Cliente(
Cod_cliente int identity(1,1) primary key not null,
Id_Persona int foreign key references Persona(Id_Persona) not null,
)

--
create table Persona(
	Id_persona int identity(1,1) primary key not null,
	DNI char(16) unique,
	PN nvarchar(20) not null,
	SN nvarchar(20) ,
	PA nvarchar(20) not null,
	SA nvarchar(20) ,
	Fecha_nacimiento date,
	Direccion nvarchar(50)  null,
	Telefono char(8)
)

--
create table Usuario(
  Id_usuario int  identity(1,1) primary key not null,
  Nombre_usuario  nvarchar(30) not null,
  Contraseña nvarchar(300) not null,
  rol varchar(50) not null,
  Estado bit not null,
  idPersona int foreign key references Persona(Id_persona)
)


--
Create table Producto(
	CodigoProducto char(5) primary key not null,
	NombreComercial nvarchar(25) not null,
	Descripcion nvarchar(50),
	Uso_terapeutico varchar(25) not null,
	PrecioVenta float not null,
	Existencia int not null,
	Id_tipo int foreign key references Tipo_producto(Id_tipo),
	idRubro int foreign key references RubroProducto(idRubro),
	id_presentacion int foreign key references PresentacionProducto(Id_presentacion),
	Concentracion varchar(15) not null,
	laboratorio varchar(35) null,
	Reseta bit null,
	Estado_producto bit not null,
	fecha_registro date not null,
	fecha_elaboracion date not null,
	fecha_vencimiento date not null
)



--
create table Tipo_producto(
IdTipo int primary key identity(1,1) not null,
Tipo nvarchar(20)
)

--
create table RubroProducto(
idRubro int identity(1,1) primary key not null,
Rubro varchar(30) not null 
)

--
create table PresentacionProducto(
	Id_presentacion int identity(1,1) primary key not null,
	Presentacion varchar(20) not null
)

--
Create table Proveedor(
Id_proveedor int identity(1,1) primary key not null,
Nombre_proveedor nvarchar(30) not null,
Direccion nvarchar(50) not null,
Estado_proveedor bit not null,
website nvarchar(50) null,
correo nvarchar(50) null,
telefono char(8) not null
)

--
Create table Compra(
Id_compra int identity(1,1) primary key not null,
Id_proveedor int foreign key references Proveedor(Id_proveedor) not null,
Id_usuario int foreign key references Usuario(Id_usuario) not null,
Fecha_compra datetime not null,
total_comprado float not null
)

--
Create table Detalle_Compra(
Id_fet_compra int identity(1,1) primary key not null,
Id_compra int foreign key references Compra(Id_compra) not null,
Cod_producto char(5) foreign key references Producto(CodigoProducto) not null,
precio_Compra float not null,
Cantidad_comprada int not null,
Subtotal float not null
)

--
create table Devolucion_Compra(
	Id_devolucion int identity(1,1) primary key not null,
	Id_compra int foreign key references Compra(Id_compra) not null,
	Fecha_devolucion datetime not null,
	total_devolucion float not null,
)

--
create table Detalle_dev_compra(
	Id_devolucion int foreign key references Devolucion_Compra(Id_devolucion) not null,
	Cod_producto char(5) foreign key references Producto(CodigoProducto) not null,
	Motivo varchar(100) null,
	Cantidad_devuelta int not null,
	sub_Total float not null,
	primary key(Id_devolucion, Cod_producto)
)

--
Create table Venta(
Id_venta int identity(1,1) primary key not null,
idCliente int foreign key references Cliente(Cod_cliente)null,
Id_usuario int foreign key references Empleado(Cod_empleado) not null,
Fecha_venta datetime not null,
Total_vendido float not null
)

--
Create table Detalle_Venta(
Id_detventa int identity(1,1) primary key,
Id_Venta int foreign key references Venta(Id_venta) not null,
Cod_producto char(5) foreign key references Producto(CodigoProducto) not null,
Cantidad_vendida int not null,
Subtotal float not null
)
go

/*PROCESOS DE ALMACENADOS*/


/*Para clientes*/
create proc Cliente_Busqueda(
	@condicion nvarchar(50) 
)
as

	select P.Id_persona, Cod_cliente, DNI,PN,SN,PA,SA,Fecha_nacimiento,Direccion,Telefono from Persona P
	inner join Cliente C
	on C.Id_Persona = P.Id_persona where Cod_cliente like @condicion+'%' or P.PN like @condicion+'%' or
	P.PA like @condicion+'%' or P.DNI like @condicion+'%' 
go


create proc Cliente_DEL(
	@Cod_cliente int
)
as
	IF EXISTS(select top 1 Cod_cliente from Cliente where Cod_cliente = @Cod_cliente)
	begin
		BEGIN TRAN 
		UPDATE Cliente set C_estado = 0 where  Cod_cliente = @Cod_cliente
	end
	ELSE
		SELECT 'Cliente no registrado' AS RESPUESTA
go

create Proc Cliente_S
as
	select  Cod_cliente, DNI,PN,SN,PA,SA,Fecha_nacimiento,Direccion,Telefono from Persona P
	inner join Cliente C
	on C.Id_Persona = P.Id_persona  
go

/*Compras*/
create proc Compra_Busqueda
(
@id_compra int)
as
if exists(select Id_compra from Compra where Id_compra =@id_compra)
	select Id_compra,P.Id_proveedor,P.Nombre_proveedor,C.Fecha_compra,C.total_comprado from Compra C
	inner join Proveedor P
	on P.Id_proveedor = C.Id_proveedor
	where Id_compra = @id_compra
else	
	print 'Compra no registrada'
go

create proc Compra_INS(
@IdProv int,
@IdUsuario int
)
as 
	if exists(select Id_proveedor from proveedor where Id_proveedor = @IdProv and Estado_proveedor = 1)
	begin
		if exists(select @IdUsuario from Usuario where Id_usuario = @IdUsuario)
		 begin
			insert into Compra(Id_proveedor, Id_usuario, Fecha_compra, total_comprado)
			values(@IdProv, @IdUsuario,getdate(),0)
		 end
		 else
			select 'Empleado no registrado' as respuesta

	end
	else
	   select 'proveedor no registrado' as respuesta
go

create proc Compra_S
as 
	select * from Compra
go

/*Detalle Compra */

create proc Detalle_Compra_INS(
	@idCompra int,
	@CodigoProd char(5),
	@precioCompra float,
	@cantidad int
)
as
	if exists(select Id_compra from Compra where Id_compra = @idCompra)
	begin
		if  exists(select CodigoProducto from Producto where CodigoProducto = @CodigoProd)
		begin
			if(@cantidad = ''  or @cantidad <= 0)
				print 'La cantidad a comprar no puede ser nula ni negativa'
			else
			begin
					Insert into Detalle_Compra(Id_compra,Cod_producto,precio_Compra,Cantidad_comprada,Subtotal)
					values(@idCompra,@CodigoProd,@precioCompra,@cantidad,dbo.Calcular_STC(@cantidad,@precioCompra))
					print 'detalle de compras registrado exitosamente'
			end
		end
		else
			print 'Producto no registrado'
	end
	else
		print 'Compra no registrada'
go

create proc Detalle_Compra_S
as
	select * from Detalle_Compra
go


create procedure Detalle_Compra_UPD
  (@id_DetalleCompra int, 
  @cod_producto char(5), 
  @nuevacantidad int,
  @precio float, 
  @accion varchar(25))
  as
  begin
	declare @cantidadactual int
	declare @cantidadInventario int
	declare @precio_unitario decimal(9,3)
	declare @diferenciapro as int 
	if(@accion = 'devolver producto')
	begin
		if exists (select Id_det_compra from Detalle_Compra where (Id_det_compra =@id_DetalleCompra))
		begin
			if exists (select CodigoProducto from Producto where CodigoProducto=@cod_producto)
			begin	
				delete from Detalle_Compra where Id_det_compra=@id_DetalleCompra		
			end
			else
			begin
				print'Producto no registrado'
				return 
			end
			print 'Producto Devuelto al Proveedor'
		end
		else
		begin
			print'Detalle_Compra No Registrado'
			return
		end
	end

	else if (@accion = 'cambiar cantidad')
	begin
	if exists (select Id_det_compra from Detalle_Compra where Id_det_compra=@id_DetalleCompra)
		begin
			if exists (select CodigoProducto from Producto where CodigoProducto=@cod_producto)
			begin
				declare @calculo float = @precio * @nuevacantidad;

				update  Detalle_Compra set Cantidad_comprada=@nuevacantidad, subtotal=@calculo 
				where Id_det_compra=@id_DetalleCompra
				print 'El cambio se realizo exitosamente'
			end
			else
			begin
				print'producto no registrado'
				return
			end
		end
		else
		begin
			print'detalle_compra no registrado'
			return
		end
	end
	else
	begin
		if(@accion='' or @accion is null)
		begin
			print'Debe Ingresar ->cambiar cantidad<-  o ->devolver<- producto para utlizar los procedimientos'
		end
	end
  end
go


 create procedure DetalleCompraBuscar
@id_detCompra int
as
declare @id_detallec as int
if exists(select Id_det_compra from Detalle_Compra where Id_det_compra = @id_detallec)
begin	
	select * from Detalle_Compra where Id_det_compra=@id_detCompra
end
else
begin	
	print'Detalle de Compra No registrada'
end

go

/*VENTAS*/

CREATE proc Venta_Busqueda(
	@id_venta int
)
as 
 if exists (select Id_venta from Venta where Id_venta = @id_venta)
	select Id_venta,V.IdCliente,V.Id_usuario,U.Nombre_usuario,V.Fecha_venta, V.Total_vendido from Venta V
	inner join Usuario U
	on U.Id_usuario  = V.Id_usuario
	inner join Persona P
	on P.Id_persona = U.idPersona
	where Id_venta = @id_venta
else
	print 'Venta no encontrada'
GO

CREATE procedure Venta_INS(
@id_Cliente int,
@idUsuario int
)
as
if exists(select Cod_empleado from Empleado where Cod_empleado = @idUsuario)
begin
	if (@id_Cliente ='')
	 begin 
		insert into Venta(IdCliente,Id_usuario,Fecha_venta,Total_vendido)
		values(NULL,@idUsuario,getdate(),0)
	 end
	 else
	 begin
		if exists (select Cod_cliente from Cliente where Cod_cliente = @id_Cliente)
		begin
			insert into Venta(IdCliente,Id_usuario,Fecha_venta,Total_vendido)
			values(@id_Cliente,@idUsuario,getdate(),0)
		end
		else
			print 'Cliente no registrado'
	 end
end	
else
	print 'Empleado no registrado'
GO

CREATE procedure Venta_INS(
@id_Cliente int,
@idUsuario int
)
as
if exists(select Cod_empleado from Empleado where Cod_empleado = @idUsuario)
begin
	if (@id_Cliente ='')
	 begin 
		insert into Venta(IdCliente,Id_usuario,Fecha_venta,Total_vendido)
		values(NULL,@idUsuario,getdate(),0)
	 end
	 else
	 begin
		if exists (select Cod_cliente from Cliente where Cod_cliente = @id_Cliente)
		begin
			insert into Venta(IdCliente,Id_usuario,Fecha_venta,Total_vendido)
			values(@id_Cliente,@idUsuario,getdate(),0)
		end
		else
			print 'Cliente no registrado'
	 end
end	
else
	print 'Empleado no registrado'
GO

create proc Venta_S
as
	SELECT * FROM Venta
go

create procedure Venta_UPD(
@id_venta int,
@id_cliente int
)
as
if exists(select Id_venta from Venta where Id_venta = @id_venta)
begin
	if(@id_cliente = '')
		update Venta set IdCliente = null
	else
	begin
		if exists(select Cod_cliente from Cliente where Cod_cliente = @id_cliente)
		update Venta set IdCliente = @id_cliente
		else
		print 'Cliente no registrado'
	end
end
else 
	print 'Venta no registrada'
go

/*Detalle Venta*/
create proc DetalleVenta_Busqueda
@idDetVenta int
as
	declare @idDetalle as int
	set @idDetalle = ( select Id_detventa from Detalle_Venta where Id_detventa = @idDetVenta)
	if(@idDetalle = @idDetVenta)
		select * from Detalle_Venta where Id_detventa = @idDetalle
	else
		print 'Detalle de venta no registrado'
go

create procedure DetalleVenta_INS(
@id_venta int,
@CodigoProd char(5),
@cantidad int 
)
as
	if not exists(select  Id_Venta from Venta where Id_venta = @id_venta)
		print 'Venta no registrada'
	else
		if not exists(select CodigoProducto from Producto where CodigoProducto = @CodigoProd)
		   print 'Producto no registrado'
		else
		begin
			if(@cantidad < 0 or @cantidad = '')
			 print 'la cantidad no puede ser negativa ni nula'
			else
			begin
				declare @ep as int
				set @ep = (select Existencia from Producto where  CodigoProducto = @CodigoProd)
				if(@cantidad > @ep)
					print 'No hay suficientes existencias'
				else
				begin
					insert into Detalle_Venta(Id_Venta ,Cod_producto,Cantidad_vendida,Subtotal)
					values(@id_venta,@CodigoProd,@cantidad,dbo.Calcular_STV(@cantidad,@CodigoProd))
				end
			end
				
		end
go

create proc DetalleVenta_S
as
	select * from Detalle_Venta
go

ALTER proc [dbo].[DetalleVenta_UPD](
	@idDetVenta int,
	@CodigoProd char(5),
	@nuevaCantidad int
)
as
	declare @cantidadActual int
	declare @cantidadInventario int
	declare @diferencia int
	declare @precioUnitario decimal(9,3)

	if exists (select Id_detventa from Detalle_Venta where Id_detventa = @idDetVenta)
	begin
		if exists(select CodigoProducto from Producto where CodigoProducto = @CodigoProd)
		begin
			set @cantidadActual = (select Cantidad_vendida from Detalle_Venta where Id_detventa = @idDetVenta)
			set @cantidadInventario = (select Existencia from Producto where CodigoProducto = @CodigoProd)
			
			if(@nuevaCantidad > @cantidadActual)
			begin
				set @diferencia = @nuevaCantidad - @cantidadActual
				if(@cantidadInventario >= @diferencia)
					update Producto set Existencia = Existencia - @diferencia where CodigoProducto = @CodigoProd
				else
					print 'No hay suficientes Existencias'
			end
			else
			begin
	
				set @diferencia = @cantidadactual - @nuevacantidad;
				update Producto set Existencia = Existencia+@diferencia where  CodigoProducto = @CodigoProd ;
			end

			select @precioUnitario = P.PrecioVenta from Producto P where CodigoProducto = @CodigoProd
			declare @NewSubTotal float = @precioUnitario * @nuevaCantidad

			update Detalle_Venta set Cod_producto = @CodigoProd, Cantidad_vendida = @nuevaCantidad, Subtotal = @NewSubTotal
			where Id_detventa = @idDetVenta
			print 'El cambio en la cantidad del producto se realizo correctamente'
			
		end
		else
			print 'Producto no registrado'
	end
 else
	   print 'Detalle de venta no registrado'
go

/*Devolucion*/

create proc DevolucionCompra_INS(
 @IdCompra int
)
as
set nocount on
if exists(select Id_compra from Compra where Id_compra= @IdCompra)
begin
 Insert into Devolucion_Compra(Id_compra,Fecha_devolucion,total_devolucion)
 values(@IdCompra, getdate(),0)
 end
 else
	print 'Compra no registrada'
go

create proc DevolucionCompra_S

as
	select * from Devolucion_Compra DV
go

/*detalle devolucion de compra*/

ALTER proc DetalleDevolucionCompra_INS(
	@CodigoProdu char(5),
	@IdDev int,
	@Cantidad int,
	@Motivo varchar(100)
)
as
set nocount on
	if exists (select CodigoProducto from Producto where CodigoProducto = @CodigoProdu)
	begin
		if exists (select Id_devolucion from Devolucion_Compra where Id_devolucion = @IdDev)
		begin
			insert into Detalle_dev_compra( Id_Devolucion,Cod_producto,Motivo,Cantidad_devuelta,sub_Total) 
			values(@IdDev, @CodigoProdu,@Motivo, @Cantidad,dbo.Calcular_STC(@cantidad,@CodigoProdu))
		end
		else
		begin
			print'Devolucion No registrada'
		end
	end
	else
	begin
		print'Producto No registrado'
	end
go

create proc DetalleDevolucionCompra_S
as
	select * from Detalle_dev_compra
go

/*Presentacion de producto*/
create proc PresentacionProducto_Busqueda
@id int
as
	select Presentacion from PresentacionProducto where Id_presentacion = @id
go

create proc  PresentacionProducto_DEL(
	@idPresentacion int 
)
as
	delete PresentacionProducto where Id_presentacion = @idPresentacion
go

create proc PresentacionProducto_INS(
	@Presentacion varchar(20) 
)
as
	if(@Presentacion= '')
		select 'No se aceptan valores nulos' as respuesta
			else
				insert into PresentacionProducto values(@Presentacion)

go

create proc PresentacionProducto_S
as
	select * from PresentacionProducto
go


/*Producto*/
create proc Producto_Buscar(
	@Codigo int
)
as
	if exists(select * from Producto where CodigoProducto = @Codigo)
		select * from Producto where CodigoProducto = @Codigo
	else
		print 'Producto no registrado'
go

create proc Producto_DEL(
	@idProducto char(5)
)
as
	if exists(select top 1 CodigoProducto from Producto where CodigoProducto = @idProducto)
	
		update Producto set Estado_producto = 0 where CodigoProducto = @idProducto
	else
		select 'producto no registrado' as respuesta
go



create proc Producto_INS(
 @Codigo char(5),
 @nombre varchar(25),
 @desc varchar(50),
 @uso varchar(25),
 @precio float,
 @existencia int,
 @idTipo int,
 @idRubro int,
 @idPresentacion int,
 @concentracion varchar(15),
 @laboratorio varchar(35),
 @reseta bit,
 @estado_p bit,
 @fecha_elaboracion date,
 @fecha_vencimiento date
)
as
	if(@Codigo = '')
		print 'El Codigo no debe ser nulo'; 
	else
	if(@nombre = '')
		print 'El producto debe llevar un nombre'
	else
		if(@precio = '' or @precio < 0)
		print 'El producto debe llevar un precio' 
		else
			if(@existencia = '' or  @existencia < 0)
				print 'El producto debe tener existencia ' 
			else
				if(@idTipo = '')
				  print 'El producto debe tener un tipo' 
				else
				    if(@idRubro = '')
					  print 'El producto debe tener un rubro' 
				    else
						if(@idPresentacion = '')
							print 'El producto debe llevar una presentacion' 
						else
							if(@concentracion = '')
							print 'El producto debe llevar  concentracion'
							else
								if (@fecha_elaboracion = '' or @fecha_vencimiento='')
								   print 'Debe ingresar las fechas del producto'
								else
								begin 
									insert into Producto
									values(@Codigo,@nombre,@desc,@uso,@precio,@existencia,@idTipo,@idRubro,@idPresentacion,@concentracion,@laboratorio,@reseta,@estado_p,getdate(),@fecha_elaboracion,@fecha_vencimiento)
								end
			
go

create proc Producto_S
as
select * from Producto where Estado_producto = 1
go


create proc Producto_UPD(
 @Codigo char(5),
 @nombre varchar(25),
 @desc varchar(50),
 @uso varchar(25),
 @precio float,
 @existencia int,
 @idTipo int,
 @idRubro int,
 @idPresentacion int,
 @concentracion varchar(15),
 @laboratorio varchar(35),
 @reseta bit,
 @fecha_elaboracion date,
 @fecha_vencimiento date
)
as
if exists(select top 1 CodigoProducto  from Producto where CodigoProducto = @Codigo)
begin

	if(@nombre = '')
		select 'El producto debe llevar un nombre' as respuesta
	else
		if(@precio = '' or @precio < 0)
		select 'El producto debe llevar un precio' as respuesta
		else
			if(@existencia = '' or @existencia < 0)
				select 'El producto debe tener existencia' as respuesta
			else
				if(@idTipo = '')
				  select 'El producto debe tener un tipo' as respuesta
				else
				    if(@idRubro = '')
					  select 'El producto debe tener un rubro' as respuesta
				    else
						if(@idPresentacion = '')
							select 'El producto debe llevar una presentacion' as respuesta
						else
							if(@concentracion = '')
							select 'El producto debe llevar  concentracion'
							else
								if (@fecha_elaboracion = '' or @fecha_vencimiento='')
									   select 'Debe ingresar las fechas del producto'
								else
								begin 
									update  Producto
									set   CodigoProducto = @Codigo,   NombreComercial = @nombre,Descripcion = @desc,Uso_terapeutico = @uso,PrecioVenta = @precio,
									Existencia = @existencia,Id_tipo = @idTipo,Id_Rubro =  @idRubro,Id_presentacion = @idPresentacion,
									Concentracion = @concentracion,Reseta =	@reseta,laboratorio = @laboratorio, fecha_elaboracion=@fecha_elaboracion,fecha_vencimiento=@fecha_vencimiento
								end
end		
else
	print 'Producto no registrado' 
go
	
/*Proveedor*/
create proc Proveedor_Busqueda(
	@IdProv int
)
as

	select Nombre_proveedor from Proveedor where Id_proveedor = @IdProv	
go


create proc Proveedor_DEL(
@IdProv int
)
as
	if exists(select top 1 Id_proveedor from Proveedor where Id_proveedor = @IdProv)
	begin
		begin tran 
		update proveedor set Estado_proveedor = 0 where Id_proveedor = @IdProv
		if @@ROWCOUNT = 1
			commit tran
			else
				rollback
	end
	else
		select 'Proveedor no registrado'
go

create proc Proveedor_INS(
	@Nombre_proveedor nvarchar(30),
	@Dir nvarchar(50),
	@website nvarchar(50),
	@correo nvarchar(50),
	@cuenta_banco nvarchar(50),
	@tel char(8)
)
as
Set nocount on
	if(@Nombre_proveedor = '')
		select 'Debes ingresar nombre proveedor' as respuesta
		else
			if(@tel = '')
			  select 'Debes ingresar el telefono' as respuesta
				else 
					if(@dir = '')
						select 'Debes ingresar la direccion' as respuesta
							else
							 Insert into Proveedor(Nombre_proveedor,Direccion,Estado_proveedor,website,correo,Cuenta_banco,Telefono)
							 values(@Nombre_proveedor,@Dir,1,@website,@correo,@cuenta_banco,@tel)

go


create proc Proveedor_S
as
	select * from Proveedor
go

create proc Proveedor_UPD(
	@idProv int,
	@NombreProv nvarchar(30),
	@Dir nvarchar(50),
	@website nvarchar(50),
	@correo nvarchar(50),
	@cuenta_banco nvarchar(50),
	@tel char(8)
)
as
Set nocount on
if exists(select top 1 Id_proveedor from Proveedor where Id_proveedor = @idProv)
begin
	if(@NombreProv = '')
		select 'Debes ingresar nombre proveedor' as respuesta
		else
			if(@tel = '')
			  select 'Debes ingresar el telefono' as respuesta
				else 
					if(@dir = '')
						select 'Debes ingresar la direccion' as respuesta
							else
							begin
							 begin tran
							 update  Proveedor
							 set Nombre_proveedor = @NombreProv,Direccion= @Dir, website = @website, correo = @correo, Cuenta_banco = @cuenta_banco, Telefono = @tel
							 where Id_proveedor = @idProv
							 if @@ROWCOUNT =1
								commit tran
								else 
									rollback
							 end
end
else 
	select 'Proveedor no registrado' as respuesta
go


create proc Rubro_Busqueda
@id int
as
	select Rubro  from RubroProducto where idRubro = @id
go

create proc Rubro_DEL
@IdRubro int
as
	if exists(select idRubro from RubroProducto where idRubro = @IdRubro)
		Delete from RubroProducto where idRubro = @IdRubro
	else
		print 'Rubro no registrado'
go

create proc Rubro_INS(
@rubro varchar(30)
)
as
	if(@rubro = '')
		select 'No puede haber valores nulos en el campor Rubro'
		   else
			insert into RubroProducto values(@rubro)
go

create proc Rubro_S
as
	select * from RubroProducto

go

create proc TipoProducto_Busqueda
@id int
as
	select * from Tipo_producto where Id_tipo =@id
go

create proc TipoProducto_DEL(
	@IdTipo int 
)
as
	DELETE Tipo_producto where Id_tipo = @IdTipo
go

create proc TipoProducto_INS(
 @Tipo nvarchar(20)
)
as
	if(@Tipo = '')
		select 'Tipo de producto no puede ser nulo' as respuesta
		else
			insert into Tipo_producto values(@tipo)
go

create proc  TipoProducto_S
as
	select * from Tipo_producto
go

create proc Usuario_Busqueda
@nuser nvarchar(30),
@pass nvarchar(300)
as
	select * from Usuario where Nombre_usuario = @nuser and Contraseña = @pass
go

create proc Usuario_BusquedaName
	@id int
	as
		select Nombre_usuario from Usuario where Id_usuario = @id
go

create proc [dbo].[Usuario_INS](
    @DNI char(16),
	@PN nvarchar(20),
    @SN nvarchar(20) ,
	@PA nvarchar(20),
	@SA nvarchar(20),
	@fechaNac date,
	@dir nvarchar(50),
	@tel char(8),
	@nuser nvarchar(30),
	@pass nvarchar(300),
	@role nvarchar(50)

)
as
	SET NOCOUNT ON
	IF NOT EXISTS(SELECT TOP 1 P.Id_persona FROM Persona P inner join Usuario U on U.idPersona = P.Id_persona WHERE DNI = @DNI  )
	BEGIN 
		if(@PA = '' or @PN = '')
			print 'El Usuario debe tener registrado un nombre y apellido'
		else
		begin
		INSERT INTO Persona(DNI,PN,SN,PA,SA,Fecha_nacimiento,Direccion,Telefono)
		VALUES(@DNI,@PN,@SN,@PA,@SA,@fechaNac,@dir,@tel)

			declare @auxIdPersona as int 
			set @auxIdPersona = @@IDENTITY
		    if(@nuser = '' or @pass = '')
				print 'Nombre de usuario y password no pueden ser nulos'
			else
			begin
				INSERT INTO Usuario(Nombre_usuario,Contraseña,rol,Estado,idPersona)
				VALUES(@nuser,@pass,@role,1,@auxIdPersona)
			end
		end

	END
	ELSE
		PRINT 'CLIENTE YA REGISTRADO'
go


/*Triggers*/

create trigger TG_DELCompra
on  
[dbo].[Detalle_Compra]
for delete
as
declare @Id_Compra as int
declare @montoNuevo as float 
declare @iddetallComp as int
declare @exits as int
declare @codigopro as int 

select @iddetallComp=Id_det_compra from deleted
select @Id_Compra=Id_Compra from deleted where Id_det_compra=@iddetallComp
select @montoNuevo = sum(subtotal) from Detalle_Compra where Id_Compra=@Id_Compra

update Compra set total_comprado=@montoNuevo where Id_Compra=@Id_Compra
 select @exits=Cantidad_comprada  from deleted where Id_det_compra=@iddetallComp
 select @codigopro=Cod_producto from deleted where Id_det_compra=@iddetallComp
update Producto set Existencia=Existencia-@exits where CodigoProducto =@codigopro
go

/*Triggers para los detalles de compra*/
create trigger TG_InventarioXCompra
on 
[dbo].[Detalle_Compra]
for insert
as


declare @idCompra as int
declare @totalComprado as float
declare @cantidadComprada as int
declare @cod_product as int

select @cantidadComprada= Cantidad_comprada from inserted 
select @cod_product=Cod_producto from inserted

update Producto set Existencia=Existencia+@cantidadComprada where CodigoProducto=@cod_product

select @idCompra= Id_Compra from inserted 
select @totalComprado = sum(Subtotal) from Detalle_Compra where Id_Compra= @idcompra
update Compra set total_comprado=@totalComprado where Id_Compra=@idcompra
go

create trigger TG_UPDDetalleCompra
on  
[dbo].[Detalle_Compra]
for update
as
declare @nventa as int
declare @montoNuevo as float 
declare @iddetallev as int
declare @id_com as int
declare @idCompr as int
select @id_com=Id_det_compra from inserted
select @idCompr=Id_Compra from inserted where Id_det_compra=@id_com
select @montoNuevo = sum(subtotal) from Detalle_Compra where Id_Compra=@idCompr
update Compra set total_comprado=@montoNuevo where Id_Compra=@idCompr
go

create trigger TG_InventarioXVenta
on 
[dbo].[Detalle_Venta]
for insert
as
declare @id_venta as int
declare @monto float
declare @cant_vendida as int
declare @id_producto as char(5)
select @cant_vendida= Cantidad_vendida from inserted 
select @id_producto = Cod_producto from inserted
update Producto set Existencia = Existencia-@cant_vendida where CodigoProducto =@id_producto
select @id_venta= Id_Venta from inserted 
select @monto = sum(Subtotal) from Detalle_Venta  where  Id_Venta= @id_venta
update Venta set Total_vendido = @monto where Id_venta = @id_venta
go


create trigger  TG_UPDTotalVenta
on  
[dbo].[Detalle_Venta]
for update
as
declare @idVenta as int
declare @montoNuevo as float 
declare @idDetVenta as int
select @idDetVenta=Id_detventa from inserted
select @idVenta=Id_Venta from inserted where Id_detventa=@idDetVenta
select @montoNuevo = sum(Subtotal) from Detalle_Venta where Id_Venta=@idVenta
update Venta set Total_vendido=@montoNuevo where Id_venta=@idVenta
go

/*FUNCIONES CALCULO DE SUBTOTAL*/

create FUNCTION Calcular_STC(@cantComprada as int, @precioCompra as float)
returns float 
AS
BEGIN
	declare @subTotalCompra as float
	set @subTotalCompra =  @cantComprada * @precioCompra
	RETURN @subTotalCompra
END
GO

CREATE FUNCTION Calcular_STV(@cantV as int, @idProd as char(5))
returns float 
AS
BEGIN
	declare @precio as float
	set @precio = (select PrecioVenta from Producto where CodigoProducto = @idProd)
	declare @SubTotalVendido as float
	set @SubTotalVendido = @cantV * @Precio
	RETURN @subTotalVendido
END
GO

--VISTA

CREATE VIEW ComprasProveedor 
AS
select c.Id_Compra, p.Nombre_proveedor, c.total_comprado, sum(dc.Cantidad_comprada) as [Cantidad comprada], c.Fecha_Compra
from Compra c 
inner join Detalle_Compra dc on dc.Id_Compra = c.Id_Compra
inner join Proveedor p on p.Id_proveedor = c.Id_proveedor
group by c.Id_Compra, p.Nombre_proveedor, c.total_comprado, c.Fecha_Compra
GO

CREATE VIEW VentasClientes 
AS
	select p.PN, p.PA, v.Id_venta,v.Total_vendido,v.Fecha_Venta  from Cliente c  
	inner join Persona P
	ON p.Id_persona = C.Id_Persona
	inner join Venta v 
	ON  v.IdCliente = C.Cod_cliente  
GO










