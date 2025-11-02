using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TvastarsOmvex.Data;
using TvastarsOmvex.Models;

namespace TvastarsOmvexMVC.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.Categories.Any())
            {
                // Categories
                var tronecs = new Category { Name = "TRONECS", Description = "Built to bear the load. Engineered to outlast." };
                var trantrack = new Category { Name = "TRANTRACK", Description = "Transfer with total accuracy." };
                var sparelink = new Category { Name = "SPARELINK", Description = "Reliable spares for unstoppable operations." };
                var flowcode = new Category { Name = "FLOWCODE", Description = "Where data meets warehouse efficiency." };
                var quadphase = new Category { Name = "QUADPHASE", Description = "From breakdown to rebuild & relocation, seamlessly." };

                context.Categories.AddRange(tronecs, trantrack, sparelink, flowcode, quadphase);
                context.SaveChanges();

                // TRONECS Products
                context.Products.AddRange(
                    new Product { Name = "BinMate", CategoryId = tronecs.Id, ShortDescription = "Small part organization with 36 bins.", LongDescription = "Mobile design for smooth inventory handling and assembly line operations.", ImagePath = "/images/products/binmate.jpg" },
                    new Product { Name = "TriBin", CategoryId = tronecs.Id, ShortDescription = "High-capacity bin storage in a compact, mobile design.", LongDescription = "Three levels and multiple bins maximize efficiency in sorting and assembly.", ImagePath = "/images/products/tribin.png" },
                    new Product { Name = "MegaBin", CategoryId = tronecs.Id, ShortDescription = "Large, accessible bin storage for bulkier items.", LongDescription = "Strong, mobile, and organized—ideal for production and warehouse applications.", ImagePath = "/images/products/megabin.jpg" },
                    new Product { Name = "LaddiCart", CategoryId = tronecs.Id, ShortDescription = "Safe ladder access with strong trolley base.", LongDescription = "Ideal for picking items from racks while carrying loads safely and efficiently.", ImagePath = "/images/products/laddicart.png" +
                    "" },
                    new Product { Name = "SmartPick", CategoryId = tronecs.Id, ShortDescription = "Intelligent PTL trolley for smart warehouses.", LongDescription = "Merges mobility with Pick-to-Light technology for error-free and fast order processing.", ImagePath = "/images/products/smartpick.png" },
                    new Product { Name = "PickMobile", CategoryId = tronecs.Id, ShortDescription = "Movable PTL stand for fast sorting and picking.", LongDescription = "Reaches any rack, ensuring flexibility and accuracy in warehouse picking operations.", ImagePath = "/images/products/pickmobile.png" },
                    new Product { Name = "SafeMove", CategoryId = tronecs.Id, ShortDescription = "Lockable cage trolley for secure transport.", LongDescription = "Strong cage design ensures organized movement of bulky goods in industries.", ImagePath = "/images/products/safemove.png" },
                    new Product { Name = "FlexiCart", CategoryId = tronecs.Id, ShortDescription = "Foldable cart for compact storage.", LongDescription = "Folds away when not in use, ideal for space-saving material handling.", ImagePath = "/images/products/flexicart.png" },
                    new Product { Name = "CargoRoll", CategoryId = tronecs.Id, ShortDescription = "Heavy-duty platform trolley.", LongDescription = "Wide platform and smooth wheels for stable movement of heavy goods.", ImagePath = "/images/products/cargoroll.png" },
                    new Product { Name = "MiniCart", CategoryId = tronecs.Id, ShortDescription = "Compact trolley for small loads.", LongDescription = "Tough yet light, ideal for quick handling in industrial spaces.", ImagePath = "/images/products/minicart.png" },
                    new Product { Name = "SideGuard", CategoryId = tronecs.Id, ShortDescription = "Partial enclosure trolley.", LongDescription = "Keeps items secure yet accessible—perfect for multipurpose transport.", ImagePath = "/images/products/sideguard.png" },
                    new Product { Name = "FoldX", CategoryId = tronecs.Id, ShortDescription = "Transformable cage trolley.", LongDescription = "Transforms from strong cage to compact storage in seconds.", ImagePath = "/images/products/foldx.png" },
                    new Product { Name = "LoadMax", CategoryId = tronecs.Id, ShortDescription = "Three-tier industrial trolley.", LongDescription = "Handles multiple loads with strength and mobility for faster material shifting.", ImagePath = "/images/products/loadmax.png" }
                );

                // TRANTRACK Products
                context.Products.AddRange(
                    new Product { Name = "FlexiFlow", CategoryId = trantrack.Id, ShortDescription = "Flexible roller conveyor.", LongDescription = "Bends and stretches to suit any layout. Ideal for cartons and lightweight goods handling.", ImagePath = "/images/products/flexiflow.png" },
                    new Product { Name = "FlexiDrive", CategoryId = trantrack.Id, ShortDescription = "Powered flexible conveyor.", LongDescription = "Combines flexibility with motorized movement for continuous operations.", ImagePath = "/images/products/flexidrive.png" },
                    new Product { Name = "InclinoFlow", CategoryId = trantrack.Id, ShortDescription = "Inclined conveyor for elevation.", LongDescription = "Smooth elevation of goods for multi-level transfers of cartons and crates.", ImagePath = "/images/products/inclinoflow.png" },
                    new Product { Name = "Telesto", CategoryId = trantrack.Id, ShortDescription = "Extendable truck loading conveyor.", LongDescription = "Reaches deep into trucks and containers, improving loading/unloading efficiency.", ImagePath = "/images/products/telesto.png" },
                    new Product { Name = "SpiraFlow", CategoryId = trantrack.Id, ShortDescription = "Gravity spiral conveyor.", LongDescription = "Uses gravity for safe downward movement. Maintenance-free and efficient.", ImagePath = "/images/products/spiraflow.png" }
                );

                // SPARELINK Products
                context.Products.AddRange(
                    new Product { Name = "Electronic Control Units & PLC Cards", CategoryId = sparelink.Id, ShortDescription = "PLC & control spares.", LongDescription = "Electronic control units, PLC cards, and controller replacements.", ImagePath = "/images/products/spare1.jpg" },
                    new Product { Name = "Industrial Wheels & Casters", CategoryId = sparelink.Id, ShortDescription = "Trolley and conveyor wheels.", LongDescription = "High-durability wheels, casters, and axles for industrial trolleys.", ImagePath = "/images/products/spare2.jpg" },
                    new Product { Name = "Power Supplies & Panels", CategoryId = sparelink.Id, ShortDescription = "Electrical spares.", LongDescription = "Power supplies, wiring, and panels for automation and conveyors.", ImagePath = "/images/products/spare3.jpg" },
                    new Product { Name = "Conveyor Spare Parts", CategoryId = sparelink.Id, ShortDescription = "Rollers, belts, drive systems.", LongDescription = "Spare conveyor rollers, belts, and drives for smooth operation.", ImagePath = "/images/products/spare4.jpg" },
                    new Product { Name = "Hydraulic & Lifting Parts", CategoryId = sparelink.Id, ShortDescription = "Hydraulic spare parts.", LongDescription = "Hydraulic systems, lifting mechanisms, and pallet truck components.", ImagePath = "/images/products/spare5.jpg" },
                    new Product { Name = "PTL Modules & Lights", CategoryId = sparelink.Id, ShortDescription = "Pick-to-Light spares.", LongDescription = "Modules, lights, and controllers for PTL systems.", ImagePath = "/images/products/spare6.jpg" }
                );

                // FLOWCODE Services
                context.Products.AddRange(
                    new Product { Name = "HMI Interface Development", CategoryId = flowcode.Id, ShortDescription = "Custom HMI design.", LongDescription = "Tailored HMI development for easy operator control.", ImagePath = "/images/products/flow1.jpg" },
                    new Product { Name = "PLC Programming", CategoryId = flowcode.Id, ShortDescription = "PLC for conveyors and trolleys.", LongDescription = "Custom PLC code for conveyors, trolleys, and automation lines.", ImagePath = "/images/products/flow2.jpg" },
                    new Product { Name = "WMS Integration", CategoryId = flowcode.Id, ShortDescription = "Warehouse Management integration.", LongDescription = "Seamless WMS and IoT connectivity for smart warehouses.", ImagePath = "/images/products/flow3.jpg" },
                    new Product { Name = "SCADA Monitoring Systems", CategoryId = flowcode.Id, ShortDescription = "Real-time monitoring.", LongDescription = "Live system performance tracking and analytics dashboards.", ImagePath = "/images/products/flow4.jpg" },
                    new Product { Name = "Barcode & RFID Integration", CategoryId = flowcode.Id, ShortDescription = "Automation data capture.", LongDescription = "Automated barcode & RFID systems for warehouse tracking.", ImagePath = "/images/products/flow5.jpg" },
                    new Product { Name = "Predictive Analytics Dashboards", CategoryId = flowcode.Id, ShortDescription = "Efficiency analytics.", LongDescription = "Real-time dashboards to improve system efficiency.", ImagePath = "/images/products/flow6.jpg" }
                );

                // QUADPHASE Services
                context.Products.AddRange(
                    new Product { Name = "System Dismantling", CategoryId = quadphase.Id, ShortDescription = "Safe dismantling services.", LongDescription = "Safe dismantling of conveyors, trolleys, PTL systems, and automation setups.", ImagePath = "/images/products/quad1.jpg" },
                    new Product { Name = "Electrical Integration", CategoryId = quadphase.Id, ShortDescription = "PLC, HMI, and sensor wiring.", LongDescription = "Electrical integration including PLCs, HMIs, sensors, and control wiring.", ImagePath = "/images/products/quad2.jpg" },
                    new Product { Name = "Rework & Retrofitting", CategoryId = quadphase.Id, ShortDescription = "Performance improvement.", LongDescription = "Reinstallation with retrofitting to enhance system performance.", ImagePath = "/images/products/quad3.jpg" },
                    new Product { Name = "Relocation Support", CategoryId = quadphase.Id, ShortDescription = "System relocation service.", LongDescription = "Safe packaging, handling, dispatch, and relocation of warehouse systems.", ImagePath = "/images/products/quad4.jpg" },
                    new Product { Name = "Testing & Commissioning", CategoryId = quadphase.Id, ShortDescription = "Post-relocation testing.", LongDescription = "Accurate reassembly, leveling, and system calibration with safety compliance.", ImagePath = "/images/products/quad5.jpg" }
                );

                context.SaveChanges();
            }


        }
    }
}
